using Application.BlazorServer.Extensions;
using Application.Models.Models;
using AutoMapper;
using BlazorStrap;
using DataManager.Libraries.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using static Application.Models.ViewModels.DispatchViewModel;

namespace Application.BlazorServer.Pages.Dispatch
{
	public partial class Dispatch
	{
		private string searchQry = "";
		private bool showSoList = false, isPosting = false, _isBusy = false;
		private bool isBusy
		{
			get
			{
				return isPosting || _isBusy;
			}
			set { _isBusy = value; }
		}

		private BSDataTable<SalesOrder> _customFilterRef = new BSDataTable<SalesOrder>();
		private readonly int _startPage = 1;
		private readonly DispatchViewModel _model = new();
		private List<SalesOrder> filteredList;
		[Inject] protected IMySqlDataAccess _sql { get; set; } = default!;
		[Inject] protected IConfiguration _conf { get; set; } = default!;
		[Inject] protected ISalesOrderService _salesOrderService { get; set; } = default!;
		[Inject] protected IDispatchService _dispatchService { get; set; } = default!;
		[Inject] protected IPalletService _palletService { get; set; } = default!;
		[Inject] protected IMapper _mapper { get; set; } = default!;
		[Inject] protected IBlazorStrap _blazorStrap { get; set; } = default!;
		[Inject] protected IJSRuntime _jsruntime { get; set; } = default!;

		#region Application Events
		[Inject] NavigationManager Navigation { get; set; } = default!;
		private HubConnection? _hubConnection;

		readonly HashSet<IDisposable> _hubRegistrations = new();
		async Task InitializeApplicationEventsConnection()
		{
			_hubConnection = new HubConnectionBuilder()
			.WithUrl(Navigation.ToAbsoluteUri("/hubs/dashboard-notification"), (opts) =>
			{
				opts.HttpMessageHandlerFactory = (message) =>
				{
					if (message is HttpClientHandler clientHandler)
						// bypass SSL certificate
						clientHandler.ServerCertificateCustomValidationCallback +=
							(sender, certificate, chain, sslPolicyErrors) => { return true; };
					return message;
				};
			})
			.Build();
			await _hubConnection.StartAsync();
		}
		#endregion

		//[CascadingParameter]
		//protected SneatHorizontalLayout? _layout { get; set; }
		public string? BarCode { get; set; }

		private bool showScanBarcode = false;
		protected override void OnInitialized()
		{
			filteredList = new();
			_model.selectedSo = new();
			var c = _conf["SapServiceLayer"];
		}
		protected async override Task OnInitializedAsync()
		{
			FetchSalesOrders();

			await InitializeApplicationEventsConnection();
		}
		private async Task FetchSalesOrders()
		{
			_model.soList = _dispatchService.GetDispatchableSalesOrders();
			SearchList("");

		}
		private bool isGranted = false;
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				//var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
				//if (status == PermissionStatus.Granted)
				//{
				//    isGranted = true;
				//}
				//else
				//{
				//    status = await Permissions.RequestAsync<Permissions.Camera>();
				//    if (status == PermissionStatus.Granted)
				//    {
				//        isGranted = true;
				//    }
				//}

				//if (isGranted)
				//{
				//    StateHasChanged();
				//    // objRef = DotNetObjectReference.Create(this);
				//    // await JSRuntime.InvokeAsync<Boolean>("jsFunctions.initScanner", objRef, "videoContainer", "videoSource", "overlay");
				//}
			}
		}
		private async Task OnScanInputEnter(KeyboardEventArgs e)
		{
			if (e.Code == "Enter" || e.Code == "NumpadEnter")
			{
				try
				{

					var value = _model.scanPalletInput;
					if (string.IsNullOrEmpty(value)) return;

					isBusy = true;
					await InvokeAsync(StateHasChanged);

					var isForRelease = _palletService.IsForRelease(value);
					if (!isForRelease)
					{
						_blazorStrap.Toaster.Add("Pallet is not for release.", o =>
						{
							o.Color = BSColor.Danger;
							o.CloseAfter = 3000;
							o.Toast = Toast.TopRight;
						});

						isBusy = false;
						await InvokeAsync(StateHasChanged);
						return;
					}

					if (_model.selectedSo.DocNum == 0)
					{
						var pallet = _palletService.Get(value);
						var salesOrder = _model.soList.Find(x => x.DocNum.Equals(pallet.SalesOrderDocNum));
						if (salesOrder is null)
						{
							_blazorStrap.Toaster.Add("No sales order found", o =>
							{
								o.Color = BSColor.Danger;
								o.CloseAfter = 3000;
								o.Toast = Toast.TopRight;
							});
							isBusy = false;
							await InvokeAsync(StateHasChanged);
							return;
						}
						await SelectSo(salesOrder);
					}


					ScanResult(value);
					await InvokeAsync(StateHasChanged);
				}
				catch (Exception err)
				{
					_blazorStrap.Toaster.Add(err.Message, o =>
					{
						o.Color = BSColor.Danger;
						o.CloseAfter = 3000;
						o.Toast = Toast.TopRight;
					});
				}
			}
		}
		private void showNotReleasedError()
		{

		}
		private void ScanResult(string data)
		{
			//BarCode = e;
			try
			{
				var target = _model.selectedSo.Pallets.Find(x => x.Label == data);
				if (target is null) throw new Exception();
				var p = _palletService.Get(data);
				var extractedData = _palletService.ExtractPalletDetails(data);
				OnPalletQtyChange(extractedData.maxBoxNo, target);
			}
			catch (Exception e)
			{
				_blazorStrap.Toaster.Add("Invalid QR", o =>
				{
					o.Color = BSColor.Danger;
					o.CloseAfter = 3000;
					o.Toast = Toast.TopRight;
				});
			}
			finally
			{
				_model.scanPalletInput = string.Empty;
				StateHasChanged();
				//_layout.ToggleBarcode();
			}

		}
		private void FilterList(ChangeEventArgs e)
		{
			SearchList(e.Value.ToString() ?? "");
		}
		private void SearchList(string value)
		{
			value = value.Trim();
			filteredList = _model.soList.Where(x =>
			{
				var include =
				x.BpName.Contains(value)
				|| x.DocDate.ToShortDateString().Contains(value)
				|| x.ItemName.Contains(value)
				|| x.DocNum.ToString().Contains(value);
				return include;
			}).ToList();
		}

		private async Task SelectSo(SalesOrder value)
		{
			_model.selectedSo = value;

			var so = _model.selectedSo;
			var pallets = new List<Pallet>();
			int qty = Convert.ToInt32(so.PlannedBoxNo);
			int boxes = so.BoxesPallet;
			int totalboxes = 0;

			if (so.BoxesPallet > 0)
			{
				for (int i = 0; i < (so.PlannedBoxNo / so.BoxesPallet); i++)
				{
					totalboxes = qty > boxes ? boxes : qty;
					pallets.Add(new()
					{
						Label = $"{so.DocNum}-{(i + 1).ToString("D3")}-{totalboxes}"
						//Label = $"{so.DocNum}-{(i + 1).ToString("D3")}-{so.PlannedBoxNo}"
					});
					qty = qty - boxes;
				}
			}
			so.Pallets.Clear();
			so.Pallets.AddRange(pallets);
		}

		private async Task ToggleSoList()
		{
			showSoList = !showSoList;
		}
		private async Task Save()
		{
			if (_model.selectedSo.PlannedBoxNo != _model.selectedSo.Pallets.Sum(x => x.Quantity))
			{
				if (!await _jsruntime.InvokeAsync<bool>("confirmWarning", "Planned no. of Boxes is not equal to actual no. of boxes. Continue saving?"))
				{
					return;
				}
			}

			isPosting = true;
			var data = _mapper.Map<DispatchModel>(_model);
			try
			{
				await _dispatchService.CreateAsync(data);

				var pallets = data.Pallets.Select(x => x.Code).ToArray();
				bool allIsReleased = _dispatchService.AllBoxIsReleased(pallets);

				if (!allIsReleased)
				{
					_blazorStrap.Toaster.Add($"Release is not yet complete. Please check the remaining items for release.", o =>
					{
						o.Color = BSColor.Warning;
						o.CloseAfter = 3000;
						o.Toast = Toast.TopRight;
					});
				}

				_hubConnection.UpdateSalesOrder(_model.selectedSo.DocEntry);

				_blazorStrap.Toaster.Add($"Posted Successfully. “Release of Pallet #{string.Join(", ", pallets)} is complete", o =>
				{
					o.Color = BSColor.Success;
					o.CloseAfter = 3000;
					o.Toast = Toast.TopRight;
				});
				await Clear();
			}
			catch (Exception ex)
			{
				_blazorStrap.Toaster.Add("Something went wrong while saving.", ex.Message, o =>
				{
					o.Color = BSColor.Danger;
					o.CloseAfter = 3000;
					o.Toast = Toast.TopRight;
				});
			}
			finally
			{
				await FetchSalesOrders();
				isPosting = false;
			}
		}
		private async Task Clear()
		{
			_model.selectedSo = new();
		}

		private void Show()
		{
			_blazorStrap.Toaster.Add("Something went wrong while saving.", "", o =>
			{
				o.Color = BSColor.Danger;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});
		}

		void OnPalletQtyChange(double value, Pallet pallet)
		{
			var sum = _model.selectedSo.Pallets.Where(x => x != pallet).Sum(x => x.Quantity);
			var maxBoxes = _model.selectedSo.PlannedBoxNo;

			var max = _model.selectedSo.BoxesPallet;
			value = value > max ? max : value;

			var sumWithNewQty = sum + value;


			if (value > max || sumWithNewQty > maxBoxes)
			{
				if (sumWithNewQty > maxBoxes)
					pallet.Quantity = (maxBoxes - sum);
				else
					pallet.Quantity = max;
			}
			else
			{
				pallet.Quantity = value;
			}
		}
	}
}