using Application.BlazorServer.Extensions;
using Application.Libraries.SAP;
using Application.Models;
using Application.Models.ViewModels;
using AutoMapper;
using BlazorStrap;
using DataManager.Models.Enums;
using DataManager.Models.InventoryTransfer;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Web;
using vm = Application.Models.ViewModels;
namespace Application.BlazorServer.Pages.InventoryTransfer;

public partial class InventoryTransfer : ComponentBase, IAsyncDisposable
{
	[Inject] private IJSRuntime _js { get; set; }
	[Inject] private IInventoryTransferService _invTransferService { get; set; }
	[Inject] private IWarehouseService _warehouseService { get; set; }
	[Inject] private IDbContextFactory<SapDb> _sapDbFactory { get; set; }
	[Inject] private IReceivingService _receivingService { get; set; }
	[Inject] private IPalletService _palletService { get; set; }
	[Inject] private ISalesOrderService _salesOrderService { get; set; }
	[Inject] private IMapper _mapper { get; set; }
	[Inject] protected IBlazorStrap _blazorStrap { get; set; } = default!;
	private InventoryTransferViewModel _model;
	private Dictionary<string, string> _UTransferType { get; set; }
	private Dictionary<string, string> _filteredLocation = new();
	private bool _isDisabled { get; set; } = true;
	private bool _isNoBin { get; set; } = false;
	private vm.InventoryTransferLine? LineSelected { get; set; }

	private RadzenDataGrid<vm.InventoryTransferLine> rdg { get; set; }
	private string _palletCode = "", _binCode = "";
	private ElementReference _lrefBincode;
	private ElementReference _lrefBtnBin;
	private int _actualQty = 0;

	string searchText = "";
	public string SearchText
	{
		get
		{
			return searchText;
		}
		set
		{
			if (searchText != value)
			{
				searchText = value;
				//searchTextStatus = $"Search text: {searchText}";
				//Console.WriteLine($"Search text: {radzenDropDown.SearchText}");
			}
		}
	}

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
		_hubConnection.InvokeAsync("UpdateSalesOrder", 2);

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
		_model = new();
		_model.inventoryTransfer.DocDate = DateTime.Now;
		InitializeApplicationEventsConnection();
	}
	protected async override Task OnInitializedAsync()
	{
		await FetchLocation();
		await FetchTransferType();
	}
	private async Task FetchLocation()
	{
		try
		{
			var data = _warehouseService.GetAll();

			_model.locations = data.ToDictionary(x => x.Id, x => x.Name);
			_filteredLocation = data.ToDictionary(x => x.Id, x => x.Name);
		}
		catch (Exception e)
		{
			_blazorStrap.Toaster.Add("Error", e.Message, o =>
			{
				o.Color = BSColor.Danger;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});
			//throw;
		}
	}
	private async Task FetchTransferType()
	{
		try
		{
			_UTransferType = await _invTransferService.GetTransferType();
		}
		catch (Exception e)
		{
			_blazorStrap.Toaster.Add("Error", e.Message, o =>
			{
				o.Color = BSColor.Danger;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});
			//throw;
		}
	}
	public void Add()
	{
		var pallet = _model.ScannedPallet;
		var line = new vm.InventoryTransferLine
		{
			ItemName = pallet.ItemName,
			PalletCode = pallet.Code,
			ActualBoxQty = pallet.Boxes.Count,
			PlannedBoxQty = pallet.PlannedQuantity,
		};
		_model.inventoryTransfer.InventoryTransferLines.Add(line);
		// clear pallet input
		_model.PalletBarcode = string.Empty;
		_model.IsBusy = false;
		_model.ScannedPallet = null;
		StateHasChanged();
	}
	private async Task Post()
	{
		_model.IsPosting = true;
		_model.IsBusy = true;
		try
		{
			//SetEmptyViewModelUnrequiredFields();
			var _data = _mapper.Map<InventoryTransferModel>(_model.inventoryTransfer);
			var data = await _invTransferService.CreateAsync(_data);
			var palletcode = _data.InventoryTransferLines.Select(x => x.PalletCode).FirstOrDefault();

			_hubConnection.UpdateSalesOrder(_model.inventoryTransfer.InventoryTransferLines.FirstOrDefault().SalesOrderNum);

			_blazorStrap.Toaster.Add("Success", $@"Transfer for Pallet #{palletcode} is complete!", o =>
			{
				o.Color = BSColor.Success;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});

			await Clear();
		}
		catch (Exception e)
		{
			_blazorStrap.Toaster.Add("Error", $@"Error on posting: {e}", o =>
			{
				o.Color = BSColor.Danger;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});

			//throw;
		}
		_model.IsBusy = false;
		_model.IsPosting = false;
	}
	private async Task OnPalletBarcodeEnter(KeyboardEventArgs e)
	{
		try
		{
			if (e.Code == "Enter")
			{
				_model.IsBusy = true;
				string barcode = _model.PalletBarcode;
				//string barcode = ee;
				if (_model.inventoryTransfer.InventoryTransferLines.Where(x => x.PalletCode == barcode).Count() > 0)
				{
					_blazorStrap.Toaster.Add("Error", "Pallet already exists", o =>
					{
						o.Color = BSColor.Danger;
						o.CloseAfter = 3000;
						o.Toast = Toast.TopRight;
					});
					_model.PalletBarcode = string.Empty;
					_model.IsBusy = false;
					return;
				}

				if (!string.IsNullOrEmpty(_model.PalletBarcode))
				{
					_model.IsBusy = true;
					await FetchPalletBinData();
					await _js.InvokeVoidAsync("FocusByElemRef", _lrefBincode);
					_model.IsBusy = false;
				}
				StateHasChanged();
				_model.IsBusy = false;
			}
		}
		catch (Exception ex)
		{
			_model.IsBusy = false;

			_blazorStrap.Toaster.Add("Info", ex.Message, o =>
			{
				o.Color = BSColor.Danger;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});
		}
	}
	public async Task FetchPalletBinData()
	{
		try
		{
			string barcode = _model.PalletBarcode;
			var pallet = _palletService.Get(barcode);
			if (pallet != null)
			{
				Func<string, string> encode = e => HttpUtility.UrlEncode(e);
				var _palletMatrix = _invTransferService.GetPalletMatrix(pallet.ItemCode, pallet.Code, pallet.BinCode, pallet.ServiceType);
				_model.BinBarcode = pallet.BinCode;
				_model.inventoryTransfer.UTransferType = _palletMatrix.TransferType;
				_model.inventoryTransfer.DisplayStatus = _palletMatrix.DisplayStatus;
				_model.inventoryTransfer.SortCodeStatus = _palletMatrix.SortCodeStatus;
				_model.inventoryTransfer.InventoryTransferLines = new List<vm.InventoryTransferLine>(); // TO DISABLE MULTIPLE PALLET
				_model.inventoryTransfer.InventoryTransferLines.Add(new vm.InventoryTransferLine
				{
					PalletCode = pallet.Code,
					BinCode = (_model.inventoryTransfer.SortCodeStatus != 3 && _model.inventoryTransfer.SortCodeStatus != 4 && _model.inventoryTransfer.SortCodeStatus != 5) ? pallet.BinCode : "", //CLEAR BIN IF THE SORT ORDER IS 3,4, or 5 -KARL 12/13/2023
					ActualBoxQty = pallet.ActualQuantity,
					PlannedBoxQty = pallet.PlannedQuantity,
					SalesOrderNum = pallet.SalesOrderDocNum,
					ItemCode = pallet.ItemCode,
					ItemName = pallet.ItemName,
					FromWarehousecode = _palletMatrix != null ? _palletMatrix.FromWarehouseCode : "",
					Warehousecode = _palletMatrix != null ? _palletMatrix.WarehouseCode : "",
				});
				_model.inventoryTransfer.LocationCode = _model.inventoryTransfer.InventoryTransferLines.Select(x => x.FromWarehousecode).FirstOrDefault();
				_model.inventoryTransfer.DestLocation = _model.inventoryTransfer.InventoryTransferLines.Select(x => x.Warehousecode).FirstOrDefault();
				_actualQty = _model.inventoryTransfer.InventoryTransferLines.Select(x => x.ActualBoxQty).FirstOrDefault();


				//_hubConnection.SetOngoing(pallet.Code, _model.inventoryTransfer.DisplayStatus);

				//REMOVE VALIDATION OF BIN IF THE SORT ORDER IS 3 OR 4 -KARL 12/13/2023
				//INCLUDE SORT ORDER 6 - KARL 07/02/2024
				if (_model.inventoryTransfer.SortCodeStatus == 2 || _model.inventoryTransfer.SortCodeStatus == 3 || _model.inventoryTransfer.SortCodeStatus == 4 || _model.inventoryTransfer.SortCodeStatus == 6)
				{
                    _isNoBin = true;
                    _isDisabled = false;
                }
			}
		}
		catch (Exception ex)
		{
			_model.IsBusy = false;
			_model.inventoryTransfer = new();
			_blazorStrap.Toaster.Add("Info", ex.Message, o =>
			{
				o.Color = BSColor.Danger;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});
		}
		_palletCode = _model.PalletBarcode;
		_binCode = _model.BinBarcode;
		_model.PalletBarcode = string.Empty;
		_model.BinBarcode = string.Empty;
	}

	public async Task CheckIsExistBincode(KeyboardEventArgs e)
	{
		if (e.Code == "Enter" || e.Code == "NumpadEnter" || e.Key == "Enter")
		{
			if (!string.IsNullOrEmpty(_palletCode) && !string.IsNullOrEmpty(_model.BinBarcode))
			{
                //BLOCK IF THE BIN NON EXISTENT -KARL 01/04/2024
                if (!_invTransferService.BinExists(_model.BinBarcode))
                {
                    _blazorStrap.Toaster.Add("Error", "Bin does not exist!", o =>
                    {
                        o.Color = BSColor.Danger;
                        o.CloseAfter = 3000;
                        o.Toast = Toast.TopRight;
                    });
                    return;
                }

                //ASSIGN NEW BIN IF THE SORT ORDER IS 5 -KARL 12/13/2023
                if (_model.inventoryTransfer.SortCodeStatus == 5)
				{
					//BLOCK IF THE BIN IS OCCUPIED
					if (_invTransferService.BinOccupied(_model.BinBarcode, _palletCode))
					{
						_blazorStrap.Toaster.Add("Error", "Bin is already occupied!", o =>
						{
							o.Color = BSColor.Danger;
							o.CloseAfter = 3000;
							o.Toast = Toast.TopRight;
						});
						return;
					}

					_model.inventoryTransfer.InventoryTransferLines.FirstOrDefault().BinCode = _model.BinBarcode;
					_binCode = _model.BinBarcode;
					_model.BinBarcode = string.Empty;
					_model.inventoryTransfer.DestLocation = _binCode.Split("-")[0];
					_model.inventoryTransfer.InventoryTransferLines.FirstOrDefault().Warehousecode = _model.inventoryTransfer.DestLocation;
                    _isDisabled = false;
                    return;
				}

				////NO NEED TO GET FROM API: ALREADY GET FROM SCANNING OF PALLET
				////var bindata = await _restService.Get<string>($"Pallets/GetIsExistBincode?palletCode={_palletCode}&binCode={_binCode}");
				if (_binCode == _model.BinBarcode)
				{
					_isDisabled = false;
					_blazorStrap.Toaster.Add("Information", $@"Bin Location {_binCode} is correct!", o =>
					{
						o.Color = BSColor.Info;
						o.CloseAfter = 3000;
						o.Toast = Toast.TopRight;
					});
					await _js.InvokeVoidAsync("FocusByElemRef", _lrefBtnBin);
				}
				else
				{
					_isDisabled = true;
					_blazorStrap.Toaster.Add("Error", $@"Incorrect bin. Please proceed to Bin {_binCode}", o =>
					{
						o.Color = BSColor.Danger;
						o.CloseAfter = 3000;
						o.Toast = Toast.TopRight;
					});
				}
			}
			else
			{
				_blazorStrap.Toaster.Add("Error", "Please scan a pallet first!", o =>
				{
					o.Color = BSColor.Danger;
					o.CloseAfter = 3000;
					o.Toast = Toast.TopRight;
				});
			}
			_model.BinBarcode = string.Empty;
		}
	}

	private async Task Clear()
	{
		_model.inventoryTransfer = new();
		_model.inventoryTransfer.DocDate = DateTime.Now;
		_isDisabled = true;
		_isNoBin = false;
    }

	public ValueTask DisposeAsync()
	{
		return DisposeApplicationEventsConnection();
	}

	public void ValidateBoxNo(ChangeEventArgs args, vm.InventoryTransferLine inventoryTransferLine)
	{
		args.Value = args.Value.ToString() == "" ? 0 : args.Value.ToString();

		if (Convert.ToInt32(args.Value) < 0)
		{
			inventoryTransferLine.ActualBoxQty = 0;
		}
		else if(Convert.ToInt32(args.Value) > _actualQty)
		{
			inventoryTransferLine.ActualBoxQty = _actualQty;
		}
		else
		{
			inventoryTransferLine.ActualBoxQty = Convert.ToInt32(args.Value);
		}

		StateHasChanged();
	}
}