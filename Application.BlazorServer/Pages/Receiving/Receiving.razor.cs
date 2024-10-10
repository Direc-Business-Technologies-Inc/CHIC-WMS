using Application.BlazorServer.Extensions;
using Application.Models;
using Application.Models.Models;
using AutoMapper;
using BlazorStrap;
using DataManager.Libraries.Repositories;
using DataManager.Models.Receiving;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using static Application.Models.ViewModels.ReceivingViewModel;

namespace Application.BlazorServer.Pages.Receiving
{
	public partial class Receiving : ComponentBase, IAsyncDisposable
	{
		private string searchQry = "";
		private bool showSoList = false, isPosting = false;

		private BSDataTable<SalesOrder> _customFilterRef = new BSDataTable<SalesOrder>();
		private readonly int _startPage = 1;
		private readonly ReceivingViewModel _model = new ReceivingViewModel();
		private List<SalesOrder> filteredList;
		[Inject]
		protected IMySqlDataAccess _sql { get; set; } = default!;
		[Inject]
		protected IConfiguration _conf { get; set; } = default!;
		[Inject]
		protected ISalesOrderService _salesOrderService { get; set; } = default!;
		[Inject]
		protected IReceivingService _receivingService { get; set; } = default!;
		[Inject]
		protected IMapper _mapper { get; set; } = default!;
		[Inject]
		protected IBlazorStrap _blazorStrap { get; set; } = default!;
		[Inject]
		protected IJSRuntime _jsruntime { get; set; } = default!;

		//[CascadingParameter]
		//protected SneatHorizontalLayout? _layout { get; set; }
		public string? BarCode { get; set; }

		private bool showScanBarcode = false;

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
		async ValueTask DisposeApplicationEventsConnection()
		{
            if (_hubRegistrations is { Count: > 0 })
            {
                foreach (var disposable in _hubRegistrations)
                {
                    disposable.Dispose();
                }
            }

            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }
        #endregion

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
			_model.soList = _receivingService.GetSalesOrderList();
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

		private void ScanResult(string data)
		{
			//BarCode = e;
			try
			{
				var target = _model.selectedSo.Pallets.Find(x => x.Label == data);
				if (target is null) throw new Exception();
				var extractedData = _receivingService.GetPalletDetails(data);
				OnPalletQtyChange(extractedData.BoxNo, target);
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
			var pallets = new List<ReceivingViewModel.Pallet>();
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
			var data = _mapper.Map<ReceivingModel>(_model);
			try
			{
				await _receivingService.CreateAsync(data);
				_blazorStrap.Toaster.Add("Posted Successfully", o =>
				{
					o.Color = BSColor.Success;
					o.CloseAfter = 3000;
					o.Toast = Toast.TopRight;
				});
				await Clear();

				int docNum;
				if(int.TryParse(data.SalesOrderNum, out docNum))
				{
					_hubConnection.UpdateSalesOrder(docNum);
				}
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

		void OnPalletQtyChange(double value, Models.ViewModels.ReceivingViewModel.Pallet pallet)
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

        public async ValueTask DisposeAsync()
        {
			DisposeApplicationEventsConnection();
        }
    }
}