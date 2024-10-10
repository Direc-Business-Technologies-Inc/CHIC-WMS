using Application.Libraries.SAP;
using Application.Libraries.SAP.SL;
using Application.MauiBlazor.Services;
using Application.MauiBlazor.Shared;
using Application.Models;
using Application.Models.ViewModels;
//using Application.Services.Repositories;
using AutoMapper;
using BlazorStrap;

using DataManager.Models.Enums;
using DataManager.Models.InventoryTransfer;
using global::Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System.Runtime.CompilerServices;
using System.Web;
using ZXingBlazor.Components;
using static Application.Models.ViewModels.ReceivingViewModel;
using InventoryTransferLine = Application.Models.ViewModels.InventoryTransferLine;
using vm = Application.Models.ViewModels;

#if ANDROID
    using Android.Views;
    using Application.MauiBlazor.Platforms.Android.PageRepositories;
#endif

namespace Application.MauiBlazor.Pages.InventoryTransfer;
public partial class Create
{
    [CascadingParameter]
    protected SneatHorizontalLayout? _layout { get; set; }
    [Inject]
    protected IBlazorStrap _blazorStrap { get; set; } = default!;
    [Inject] private IJSRuntime _js { get; set; }
    //[Inject] private IInventoryTransferService _invTransferService { get; set; }
    //[Inject] private IWarehouseService _warehouseService { get; set; }
    [Inject] private IDbContextFactory<SapDb> _sapDbFactory { get; set; }
    //[Inject] private IReceivingService _receivingService { get; set; }
    //[Inject] private IPalletService _palletService { get; set; }
    [Inject] protected IConfiguration _conf { get; set; }
    [Inject] protected RestServiceFactory _httpClientFactory { get; set; }
    [Inject] protected IMapper _mapper { get; set; } = default!;

//#if ANDROID
//        [Inject] protected IOnPageKeyDown _pgRepo { get; set; }
//#endif

    private RestService _restService { get; set; }

    private InventoryTransferViewModel _model;
    private List<vm.BatchSerialViewModel.BatchSerial> filteredSrcList { get; set; }
    private List<vm.BatchSerialViewModel.BatchSerial> filteredDesList { get; set; }
    private IList<vm.BatchSerialViewModel.BatchSerial> _batchSelectedSrc { get; set; }
    private IList<vm.BatchSerialViewModel.BatchSerial> _batchSelectedDes { get; set; }
    private vm.InventoryTransferLine? LineSelected { get; set; }
    private Dictionary<string, string> _filteredFromLoc { get; set; }
    private Dictionary<string, string> _filteredLocation { get; set; }
    private Dictionary<string, string> _UTransferType { get; set; }
    private RadzenDataGrid<vm.InventoryTransferLine> rdg { get; set; }
    private string _pressedKey;
    private string _palletCode { get; set; } = "";
    private string _binCode { get; set; } = "";
    private bool showBatchSrcList { get; set; } = false;
    private bool showBatchDesList { get; set; } = false;
    private bool _isDisabled { get; set; } = true;
    private bool _isNoBin { get; set; } = false;
    private bool _fldDisabled { get; set; } = true;
    private int _curLine { get; set; }
    private bool isGranted = false;
	private int _actualQty = 0;

    private string _scanType { get; set; } = "";
    #region UI_COMPONENT
    /// <summary>
    /// Element Initialization
    /// </summary>
    private ElementReference _lrefPalletcode;
	private ElementReference _lrefBincode;
    private ElementReference _lrefBtnBatch;
    private RadzenTextBox _radzPalletcode;
    private RadzenTextBox _radzBincode;
    private RadzenDataGrid<vm.BatchSerialViewModel.BatchSerial> grid;
    private Variant variant = Variant.Outlined;
	#endregion

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

    protected override async Task OnParametersSetAsync()
    {

    }
    protected override void OnInitialized()
    {
        filteredSrcList = new();
        _model = new();
        _model.inventoryTransfer.DocDate = DateTime.Now;
        string baseAddr = _conf["WebApiEndpoint"];
        _restService = _httpClientFactory.Create(baseAddr);

		string signalRAddr = _conf["SignalREndpoint"];

		InitializeApplicationEventsConnection(signalRAddr);
	}
    protected async override Task OnInitializedAsync()
    {
        _layout.OnAddCallback = new EventCallback(this, Add);
        await FetchLocation();
        //FilterLocationsByTransferType(TransferTypeEnum.ForStorage);
        await FetchTransferType();
    }
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

        _lrefPalletcode = _radzPalletcode.Element;
        _lrefBincode = _radzBincode.Element;
    }

    //protected override Task OnAfterRenderAsync(bool firstRender)
    //{
    //    _lrefPalletcode = _radzPalletcode.Element;
    //    _lrefBincode = _radzBincode.Element;
    //    return base.OnAfterRenderAsync(firstRender);
    //}

//#if ANDROID
//        public bool OnPageKeyDown(Keycode keyCode)
//        {
//            switch (keyCode)
//            {

//                case Keycode.NumpadEnter:
//                 // Your code here
//                return true;

//                case Keycode.Enter:
//                 // Your code here
//                return true;

//                default:
//                   return false;
//           }
//        }
//#endif

    private async Task FetchLocation()
    {
        try
        {
            string baseAddr = _conf["WebApiEndpoint"];
            //_restService = _httpClientFactory.Create(baseAddr);
            var data = await _restService.Get<List<Models.Warehouse>>("Warehouses");
            //_model.locations = _warehouseService.GetAll().ToDictionary(x => x.Id, x => x.Name);
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
        }
    }
    private async Task FetchTransferType()
    {
        try
        {
            _UTransferType = await _restService.Get<Dictionary<string, string>>("InventoryTransfer/GetTransferType");
            //_model.UTransferTypes = dd.Select(x=> new InventoryTransferViewModel.UTransferType { Code = x.Key, Name = x.Value}).ToList();
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
    private async Task FetchServiceType(string serviceType)
    {
        try
        {
            _model.servicedata = await _restService.Get<InventoryTransferViewModel.ServiceData>($@"InventoryTransfer/SERVICE_DATA?serviceType={serviceType}");
        }
        catch (Exception e)
        {
            //throw;
        }
    }
    private async Task FetchBatchData(int lineNum)
    {
        try
        {
            if (filteredSrcList.Count == 0)
            {
                var lineSel = _model.inventoryTransfer.InventoryTransferLines.Where(x => x.LineNum == lineNum).FirstOrDefault();
                filteredSrcList = await _restService.Get<List<BatchSerialViewModel.BatchSerial>>($@"BatchSerial/GetBatchSerialByMnfSerialLoc?itemCode={lineSel.ItemCode}&mnfSerial={lineSel.PalletCode}&location={lineSel.FromWarehousecode}");
                filteredSrcList.ForEach(fe =>
                {
                    fe.Quantity = Math.Round(fe.Quantity ?? 0);
                });
                _curLine = lineNum;
            }
            ToggleBatchSrcList();
        }
        catch (Exception ex)
        {
            var strSplit = SplitToArr(ex.Message);
            _blazorStrap.Toaster.Add("Info", strSplit.Length > 0 ? strSplit[1] : ex.Message, o =>
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

    private void ViewBatches(vm.InventoryTransferLine line)
    {
        LineSelected = line;
    }

    private void RemoveLine(vm.InventoryTransferLine line)
    {
        _model.inventoryTransfer.InventoryTransferLines.Remove(line);
        rdg.Reload();
    }

    private async Task Post()
    {
        _model.IsPosting = true;
        _model.IsBusy = true;
        try
        {
            //SetEmptyViewModelUnrequiredFields();
            var _data = _mapper.Map<InventoryTransferModel>(_model.inventoryTransfer);
            var palletcode = _data.InventoryTransferLines.Select(x => x.PalletCode).FirstOrDefault();
            var data = await _restService.ExecutePostAsync<DataManager.Models.InventoryTransfer.InventoryTransferModel>("InventoryTransfer", _data);
            if (data.IsSuccessful)
            {
				_hubConnection.InvokeAsync("UpdateSalesOrder", _model.inventoryTransfer.InventoryTransferLines.FirstOrDefault().SalesOrderNum);

				_blazorStrap.Toaster.Add("Success", $@"Transfer for Pallet #{palletcode} is complete!", o =>
                {
                    o.Color = BSColor.Success;
                    o.CloseAfter = 3000;
                    o.Toast = Toast.TopRight;
                });

                await Clear();
            }
            else
            {
                _blazorStrap.Toaster.Add("Error", "Error on posting", o =>
                {
                    o.Color = BSColor.Danger;
                    o.CloseAfter = 3000;
                    o.Toast = Toast.TopRight;
                });
            }
        }
        catch (Exception e)
        {
            //throw; //DO NOT THROW; OTHERWISE UR APP WILL NOT RESPONSE UPON LOADING
        }
        _model.IsBusy = false;
        _model.IsPosting = false;
    }
    public void SetEmptyViewModelUnrequiredFields()
    {
        if (_model.inventoryTransfer != null)
        {
            foreach (var line in _model.inventoryTransfer.InventoryTransferLines)
            {
                line.BinCode = "";
                //line.FromWarehousecode = "";
                //line.Warehousecode = "";

                foreach (var btch in line.BatchNumbers)
                {
                    btch.BatchNumber = "";
                    btch.ManufacturerSerialNumber = "";                    
                    //btch.Quantity = 0;
                }
            }
        }
    }
    private async Task Keydown(KeyboardEventArgs e)
    {
		var key = _js.InvokeVoidAsync("myKeyPress", _radzPalletcode).GetType();
		if (e.Key == "Tab")
        {
			await GetCheckPallet();
		}
	}
    
    private async Task OnPalletBarcodeEnter(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter" || e.Key == "Enter")
        {
            await GetCheckPallet();
		}
    }

    private async Task GetCheckPallet()
    {
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
	}
    public async Task FetchPalletBinData()
    {
        try
        {
            string barcode = _model.PalletBarcode;
            var pallet = await _restService.Get<Models.Pallet>($"Pallets/{barcode}");
            if (pallet != null)
            {                
                Func<string, string> encode = e=> HttpUtility.UrlEncode(e);
                ////VALIDATION FROM UDO MATRIX
                var _palletMatrix = await _restService.Get<Models.Pallet>($"InventoryTransfer/GetPalletMatrix?itemCode={encode(pallet.ItemCode)}&palletCode={encode(pallet.Code)}&serviceType={encode(pallet.ServiceType)}&binCode={encode(pallet.BinCode)}");
                _model.BinBarcode = pallet.BinCode;
                _model.inventoryTransfer.UTransferType = _palletMatrix.TransferType;
                _model.inventoryTransfer.DisplayStatus = _palletMatrix.DisplayStatus;
                _model.inventoryTransfer.SortCodeStatus = _palletMatrix.SortCodeStatus;
                _model.inventoryTransfer.InventoryTransferLines = new List<InventoryTransferLine>(); // TO DISABLE MULTIPLE PALLET
                _model.inventoryTransfer.InventoryTransferLines.Add(new InventoryTransferLine
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

            var strSplit = SplitToArr(ex.Message);
            _blazorStrap.Toaster.Add("Info", strSplit.Length > 0 ? strSplit[1] : ex.Message, o =>
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
                if (!await _restService.Get<bool>($"InventoryTransfer/BinExists?binCode={_model.BinBarcode}"))
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
                    //if (_invTransferService.BinOccupied(_model.BinBarcode))
                    if (await _restService.Get<bool>($"InventoryTransfer/BinOccupied?binCode={_model.BinBarcode}&palletCode={_palletCode}"))
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
                    //await _js.InvokeVoidAsync("FocusByElemRef", _lrefBtnBin); 
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

    private void ScanResult(string data)
    {
        //BarCode = e;
        try
        {
            if(_scanType == "Pallet")
            {
                _model.PalletBarcode = data;
            }
            else if (_scanType == "Bin")
            {
                _model.BinBarcode = data;
            }
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
            _layout.ToggleBarcode();
        }

    }

    private void FilterList(ChangeEventArgs e)
    {
        SearchList(e.Value.ToString() ?? "");
    }
    private void SearchList(string value)
    {
        //value = value.Trim();
        //filteredList = _model.soList.Where(x =>
        //{
        //    var include =
        //    x.BpName.Contains(value)
        //    || x.DocDate.ToShortDateString().Contains(value)
        //    || x.ItemName.Contains(value)
        //    || x.DocNum.ToString().Contains(value);
        //    return include;
        //}).ToList();
    }

    private async Task Clear()
    {
        _model.inventoryTransfer = new();
		_model.inventoryTransfer.DocDate = DateTime.Now;
        _isDisabled = true;
		_isNoBin = false;
	}
    private void ToggleBatchSrcList()
    {
        showBatchSrcList = !showBatchSrcList;
        if (_batchSelectedSrc == null) return;

        ////ASSIGN THE SELECTED BATCH TO THE BINDED MODEL
        foreach (var line in _model.inventoryTransfer.InventoryTransferLines)
        {
            if (line.LineNum == _curLine)
            {
                line.ActualBoxQty = decimal.ToInt32(_batchSelectedSrc.Select(sel => sel.Quantity).Sum() ?? 0);
                line.BatchNumbers = _batchSelectedSrc.Where(x => x.BaseLineNumber == _curLine)
                                                    .Select(x => new InventoryTransferBatch
                                                    {
                                                        BatchNumber = x.DistNumber,
                                                        Quantity = decimal.ToInt32(x.Quantity ?? 0),
                                                        ManufacturerSerialNumber = x.MnfSerial,
                                                        InternalSerialNumber = x.LotNumber,
                                                    }).ToList();
                line.StockTransferLinesBinAllocations = _batchSelectedSrc.Where(x => x.BaseLineNumber == _curLine)
                                                        .Select(x => new DataManager.Models.Bins.BinAllocation
                                                        {
                                                            BinAbsEntry = x.BinAbs,
                                                            Quantity = x.Quantity ?? 0,
                                                            BaseLineNumber = x.BaseLineNumber,
                                                        }).ToList();
                return;
            }
        };
    }

    private void FilterLocationsByTransferType(TransferTypeEnum type)
    {
        string loc = "";
        switch (type)
        {
            case TransferTypeEnum.ForStorage:
                _filteredFromLoc = (from t0 in _model.locations where t0.Key == "RC" || t0.Key == "U" select t0).ToDictionary(x => x.Key, x => x.Value);

                _model.inventoryTransfer.ShowLocation = !true;
                _model.inventoryTransfer.ShowDestLocation = !false;
                loc = _model.locations.Where(x => x.Key == "RC").Select(x => x.Key).FirstOrDefault();
                if (!string.IsNullOrEmpty(loc)) _model.inventoryTransfer.LocationCode = loc;
                break;
            case TransferTypeEnum.ForIrradiation:
                _filteredFromLoc = (from t0 in _model.locations where t0.Value.Contains("STORAGE", StringComparison.InvariantCultureIgnoreCase) || t0.Key == "RC" select t0).ToDictionary(x => x.Key, x => x.Value);
                _filteredLocation = (from t0 in _model.locations where t0.Key == "L" select t0).ToDictionary(x => x.Key, x => x.Value);

                _model.inventoryTransfer.ShowLocation = !false;
                _model.inventoryTransfer.ShowDestLocation = !true;
                break;
            case TransferTypeEnum.AtIrradiation:
                _filteredLocation = (from t0 in _model.locations where t0.Key == "L" select t0).ToDictionary(x => x.Key, x => x.Value);

                _model.inventoryTransfer.ShowLocation = !false;
                _model.inventoryTransfer.ShowDestLocation = !true;
                loc = _model.locations.Where(x => x.Key == "L").Select(x => x.Key).FirstOrDefault();
                if (!string.IsNullOrEmpty(loc)) _model.inventoryTransfer.DestLocation = loc;
                break;
            default:
                _filteredFromLoc = _model.locations;
                _filteredLocation = _model.locations;
                _model.inventoryTransfer.ShowLocation = !true;
                _model.inventoryTransfer.ShowDestLocation = !true;
                break;
        }

        //if (_filteredLocation.Count < 1) return;
        //_model.inventoryTransfer.LocationCode = _filteredLocation.First().Key;
        StateHasChanged();
    }
    public string[] SplitToArr(string str)
    {
        string[] strArr = str.Split(new string[] { $@"""" }, StringSplitOptions.None);
        return strArr;
    }
}
