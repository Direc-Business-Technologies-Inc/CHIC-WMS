using Application.Models.Models;
using Application.Models.ViewModels;
//using Application.Services.Repositories;
using AutoMapper;
using BlazorStrap.V5;
using BlazorStrap;
using DataManager.Libraries.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using static Application.Models.ViewModels.DispatchViewModel;
using Application.MauiBlazor.Services;
//using Application.Services.Core;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Application.MauiBlazor.Shared;

namespace Application.MauiBlazor.Pages.Dispatch
{

	public partial class Dispatch
	{
		private string searchQry = "";
		private bool showSoList = false, isPosting = false;

		private BSDataTable<SalesOrder> _customFilterRef = new BSDataTable<SalesOrder>();
		private readonly int _startPage = 1;
		private readonly DispatchViewModel _model = new();

		private List<SalesOrder> filteredList;
		[CascadingParameter]
		protected SneatHorizontalLayout? _layout { get; set; }
		[Inject] protected IMySqlDataAccess _sql { get; set; } = default!;
		[Inject] protected IConfiguration _conf { get; set; } = default!;
		//[Inject] protected ISalesOrderService _salesOrderService { get; set; } = default!;
		//[Inject] protected IDispatchService _dispatchService { get; set; } = default!;
		//[Inject] protected IPalletService _palletService { get; set; } = default!;
		[Inject] protected IMapper _mapper { get; set; } = default!;
		[Inject] protected IBlazorStrap _blazorStrap { get; set; } = default!;
		[Inject] protected IJSRuntime _jsruntime { get; set; } = default!;
		[Inject] protected RestServiceFactory _httpClientFactory { get; set; }
		private RestService _restService { get; set; }

		//[CascadingParameter]
		//protected SneatHorizontalLayout? _layout { get; set; }
		public string? BarCode { get; set; }

		private bool showScanBarcode = false;

		#region Application Events
		[Inject] NavigationManager Navigation { get; set; } = default!;
		private HubConnection? _hubConnection;

		readonly HashSet<IDisposable> _hubRegistrations = new();
		async Task InitializeApplicationEventsConnection(string baseAddress)
		{
			_hubConnection = new HubConnectionBuilder()
			.WithUrl(Navigation.ToAbsoluteUri(baseAddress + "hubs/dashboard-notification"), (opts) =>
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

		protected override void OnInitialized()
		{
			filteredList = new();
			_model.selectedSo = new();
			var c = _conf["SapServiceLayer"];
			string baseAddr = _conf["WebApiEndpoint"];
			_restService = _httpClientFactory.Create(baseAddr);

			string signalRAddr = _conf["SignalREndpoint"];

			InitializeApplicationEventsConnection(signalRAddr);
		}
		protected async override Task OnInitializedAsync()
		{
			FetchSalesOrders();
		}
		private async Task FetchSalesOrders()
		{
			_model.soList = await _restService.Get<List<SalesOrder>>("Dispatch/GetDispatchableSalesOrder");
			SearchList("");

		}
		private bool isGranted = false;
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
				if (status == PermissionStatus.Granted)
				{
					isGranted = true;
				}
				else
				{
					status = await Permissions.RequestAsync<Permissions.Camera>();
					if (status == PermissionStatus.Granted)
					{
						isGranted = true;
					}
				}

				if (isGranted)
				{
					StateHasChanged();
					// objRef = DotNetObjectReference.Create(this);
					// await JSRuntime.InvokeAsync<Boolean>("jsFunctions.initScanner", objRef, "videoContainer", "videoSource", "overlay");
				}
			}
		}

		private void ScanResult(string data)
		{
			//BarCode = e;
			try
			{
				var target = _model.selectedSo.Pallets.Find(x => x.Label == data);
				if (target is null) throw new Exception();
				//var extractedData = _palletService.ExtractPalletDetails(data);
				var extractedData = _restService.Get<object>($"Pallet/ExtractPalletDetails?data={data}").Result;
				OnPalletQtyChange(1, target);
				//OnPalletQtyChange(extractedData.maxBoxNo, target);
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
			// TODO: auto mapper configuration
			var data = _mapper.Map<DispatchModel>(_model);
			try
			{
				// TODO: Posting
				var resp = await _restService.ExecutePostAsync("Dispatch", data);
                if(!resp.IsSuccessful)
				{
					string error = JsonConvert.DeserializeObject<string>(resp.Content);
					throw new Exception(error);
                }
                var pallets = data.Pallets.Select(x => x.Code).ToArray();
                bool allIsReleased = await _restService.Post<bool>("Dispatch/AllBoxIsReleased", pallets);

                if (!allIsReleased)
                {
                    _blazorStrap.Toaster.Add($"Release is not yet complete. Please check the remaining items for release.", o =>
                    {
                        o.Color = BSColor.Warning;
                        o.CloseAfter = 3000;
                        o.Toast = Toast.TopRight;
                    });
                }

				_hubConnection.InvokeAsync("UpdateSalesOrder", _model.selectedSo.DocNum);

				_blazorStrap.Toaster.Add($"Posted Successfully. Release of Pallet #{string.Join(", ", pallets)} is complete", o =>
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
			StateHasChanged();
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
        private async void OnScanInputEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
				await HandleEnter();
            }
        }
		private async Task HandleEnter()
		{
            try
            {
                var value = _model.scanPalletInput;
                if (string.IsNullOrEmpty(value)) return;
                //var isForRelease = await _restService.Post<bool>("Dispatch/IsForRelease", value);
                var isForRelease = await _restService.Get<bool>($"Pallets/IsForRelease?palletCode={value}");
                if (!isForRelease && false)
                {
                    _blazorStrap.Toaster.Add("Pallet is not for release.", o =>
                    {
                        o.Color = BSColor.Danger;
                        o.CloseAfter = 3000;
                        o.Toast = Toast.TopRight;
                    });
                    return;
                }

                if (_model.selectedSo.DocNum == 0)
                {
					var pallet = await _restService.Get<Application.Models.Pallet>($"Pallets/{value}");
                    var salesOrder = _model.soList.Find(x => x.DocNum.Equals(pallet.SalesOrderDocNum));
                    if (salesOrder is null)
                    {
                        _blazorStrap.Toaster.Add("No sales order found", o =>
                        {
                            o.Color = BSColor.Danger;
                            o.CloseAfter = 3000;
                            o.Toast = Toast.TopRight;
                        });
                        await InvokeAsync(StateHasChanged);
                        return;
                    }
                    await SelectSo(salesOrder);
                }

                ScanResult(value);
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