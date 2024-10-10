using Application.Libraries.SAP;
using Application.Libraries.SAP.SL;
using Application.MauiBlazor.Services;
using Application.MauiBlazor.Shared;
using Application.Models;
using Application.Models.ViewModels;
//using Application.Services.Repositories;
using AutoMapper;
using BlazorStrap;
using BlazorStrap.V5;
using Dapper;
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
using static Application.Models.ViewModels.QCMaintenanceViewModel;
using InventoryTransferLine = Application.Models.ViewModels.InventoryTransferLine;
using vm = Application.Models.ViewModels;

//#if ANDROID
//    using Android.Views;
//    using Application.MauiBlazor.Platforms.Android.PageRepositories;
//#endif

namespace Application.MauiBlazor.Pages.InventoryTransfer
{
    public partial class ManualTransfer 
    {

        [CascadingParameter] protected SneatHorizontalLayout? _layout { get; set; }
        [Inject] protected IBlazorStrap _blazorStrap { get; set; } = default!;
        [Inject] private IJSRuntime _js { get; set; }
        //[Inject] private IInventoryTransferService _invTransferService { get; set; }
        //[Inject] private IWarehouseService _warehouseService { get; set; }
        [Inject] private IDbContextFactory<SapDb> _sapDbFactory { get; set; }
        //[Inject] private IReceivingService _receivingService { get; set; }
        //[Inject] private IPalletService _palletService { get; set; }
        [Inject] protected IConfiguration _conf { get; set; }
        [Inject] protected RestServiceFactory _httpClientFactory { get; set; }
        [Inject] protected IMapper _mapper { get; set; } = default!;
        [Inject] protected DialogService _dialogService { get; set; }

//#if ANDROID
//        [Inject] protected IOnPageKeyDown _pgRepo { get; set; }
//#endif

        private RestService _restService { get; set; }

        private InventoryTransferViewModel _model;
        private List<vm.BatchSerialViewModel.BatchSerial> filteredBatchList { get; set; }
        private List<vm.BinViewModel.BinMappingViewModel.BinAccumulator> filteredBinList { get; set; }
        private List<vm.BinViewModel.BinMappingViewModel.BinAccumulator> filteredBinDestList { get; set; }
        private List<vm.BatchSerialViewModel.BatchSerial> _batchSelectedSrc { get; set; } = new List<vm.BatchSerialViewModel.BatchSerial>();
        private List<vm.BinViewModel.BinMappingViewModel.BinAccumulator> _binSelected { get; set; } = new List<vm.BinViewModel.BinMappingViewModel.BinAccumulator>();

        private vm.InventoryTransferLine? LineSelected { get; set; }
        private Dictionary<string, string> _filteredFromLoc { get; set; }
        private Dictionary<string, string> _filteredLocation { get; set; }
        private Dictionary<string, string> _UTransferType { get; set; }
        private RadzenDataGrid<vm.InventoryTransferLine> rdg { get; set; }
        private string searchQry { get; set; } = ""; 
        private string _itemCode { get; set; } = "";
        private string _palletCode { get; set; } = "";
        private string _binCode { get; set; } = "";
        private bool _isDisabled { get; set; } = true;
		private bool _isNoBin { get; set; } = false;
		private int _curLine { get; set; }
        private string _curBatch { get; set; }
        private string _curBin { get; set; }
        private bool isGranted = false;
        private int _showMode { get; set; } = 0;

        private string _scanType { get; set; } = "";
        #region UI_COMPONENT
        /// <summary>
        /// Element Initialization
        /// </summary>
        private ElementReference _lrefPalletcode;
        private ElementReference _lrefBtnPallet;
        private ElementReference _lrefBincode;
        private ElementReference _lrefBtnBin;
        private ElementReference _lrefBtnSave;
        private BSButton _bsBtnSave;
        private RadzenTextBox _radzTxtPallet;
        private RadzenTextBox _radzTxtBin;
        private RadzenButton _radzBtnPallet;
        private RadzenButton _radzBtnBin;
        private RadzenDataGrid<vm.BatchSerialViewModel.BatchSerial> gridBatch;       
        private RadzenDataGrid<vm.BinViewModel.BinMappingViewModel.BinAccumulator> gridBin;
        private RadzenDataGrid<vm.BinViewModel.BinMappingViewModel.BinAccumulator> gridBinDest;
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

		protected override async Task OnParametersSetAsync()
        {

        }
        protected override void OnInitialized()
        {
            filteredBatchList = new();
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

            _lrefPalletcode = _radzTxtPallet.Element;
            _lrefBincode = _radzTxtBin.Element;
            _lrefBtnPallet = _radzBtnPallet.Element;
            _lrefBtnBin = _radzBtnBin.Element;
        }

        //protected override Task OnAfterRenderAsync(bool firstRender)
        //{
        //    ////Passing the element Properties from Radzen Component
        //    _lrefPalletcode = _radzTxtPallet.Element;
        //    _lrefBincode = _radzTxtBin.Element;
        //    _lrefBtnPallet = _radzBtnPallet.Element;
        //    _lrefBtnBin = _radzBtnBin.Element;
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
                _blazorStrap.Toaster.Add("Error", e.Message, o =>
                {
                    o.Color = BSColor.Danger;
                    o.CloseAfter = 3000;
                    o.Toast = Toast.TopRight;
                });
                //throw;
            }
        }
        private async Task FetchBatchData(int lineNum)
        {
            try
            {
                _curLine = lineNum;
                _model.IsBusy = true;
                ////BinSelected get from ivntytransferviewmodel.invtytransferline.BinAllocation
                var lineSel = _model.inventoryTransfer.InventoryTransferLines.Where(x => x.LineNum == lineNum).FirstOrDefault();
                var batchLine = _model.inventoryTransfer.InventoryTransferLines.AsEnumerable().Where(x => x.LineNum == _curLine)
                                     .Select(x => x.BatchNumbers);

                _batchSelectedSrc = new();
                foreach (var batches in batchLine)
                {
                    foreach (var batch in batches)
                    {
                        _batchSelectedSrc.Add(new BatchSerialViewModel.BatchSerial
                        {
                            DistNumber = batch.BatchNumber,
                            //BinCode = filteredBinList.Where(sel => sel.BinAbs == bin.BinAbsEntry).Select(sel => sel.BinCode).FirstOrDefault(),
                            OnHandQty = batch.Quantity,
                        });
                    }
                };

                filteredBatchList = await _restService.Get<List<BatchSerialViewModel.BatchSerial>>($@"BatchSerial/GetBatchSerialByMnfSerialLoc?itemCode={lineSel.ItemCode}&mnfSerial={lineSel.PalletCode}&location={lineSel.FromWarehousecode}");
                filteredBatchList.ForEach(fe =>
                {
                    fe.Quantity = Math.Round(fe.Quantity??0);
                    fe.CheckState = _batchSelectedSrc == null ? false : _batchSelectedSrc.Where(sel => sel.DistNumber == fe.DistNumber).Any();
                });
                                                  
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
            _showMode = 1;            
            _model.IsBusy = false;
        }
        public void CheckAllBatch(bool args)
        {
            filteredBatchList.ForEach(fe => fe.CheckState = args);
        }
        public void CheckBatch(bool args)
        {
            var lineSel = _model.inventoryTransfer.InventoryTransferLines.Where(x => x.LineNum == _curLine).FirstOrDefault();
            var cnt = gridBatch.View.Where(x => x.CheckState == true).Count();
            if (cnt > lineSel.PlannedBoxQty)
            {
                _blazorStrap.Toaster.Add("Info", "Total selected batch is over the planned Qty!", o =>
                {
                    o.Color = BSColor.Danger;
                    o.CloseAfter = 3000;
                    o.Toast = Toast.TopRight;
                });
            }
        }
        public void CheckBin(bool args)
        {
            var lineSel = _model.inventoryTransfer.InventoryTransferLines.Where(x => x.LineNum == _curLine).FirstOrDefault();
            var cnt = gridBin.View.Where(x => x.CheckState == true).Count();
            if (cnt > lineSel.PlannedBoxQty)
            {
                _blazorStrap.Toaster.Add("Info", "Total selected batch is over the planned Qty!", o =>
                {
                    o.Color = BSColor.Danger;
                    o.CloseAfter = 3000;
                    o.Toast = Toast.TopRight;
                });
            }
        }
        public async Task CheckDestBin(bool args, string binCode)
        {
            var lineSel = _model.inventoryTransfer.InventoryTransferLines.Where(x => x.LineNum == _curLine).FirstOrDefault();
            var batchSelQty = filteredBatchList.Where(x => x.DistNumber == _curBatch).Select(x => x.Quantity)?.FirstOrDefault() ?? 0;
            var cnt = gridBinDest.View.Where(x => x.CheckState == true).Count();
            if (cnt > batchSelQty)
            {
                //await gridBinDest.View.Where(x => x.BinCode == binCode).ForEachAsync(fe => fe.CheckState = false);
                filteredBinDestList.Where(sel=> sel.BinCode == binCode).ToList().ForEach(fe =>
                {
                    fe.CheckState = false;
                });
                if (filteredBinDestList != null) await gridBinDest.RefreshDataAsync();
                _blazorStrap.Toaster.Add("Info", "Selected bin is over from the Batch Qty!", o =>
                {
                    o.Color = BSColor.Danger;
                    o.CloseAfter = 3000;
                    o.Toast = Toast.TopRight;
                });
            }
            else
            {
                //await gridBinDest.View.Where(fe => fe.CheckState == true).ForEachAsync(fe => fe.OnHandQty = fe.CheckState == true ? 1 : 0);
                filteredBinDestList.ForEach(fe =>
                {
                    fe.OnHandQty = fe.CheckState == true ? 1 : 0;
                });
            }
           
        }
        private async Task FetchBinDestList(string itemCode, string distNumber, string whsCode, int baseLine)
        {
            try
            {
                _curBatch = distNumber;
                _model.IsBusy = true;
                if (_binSelected != null) _binSelected.Clear(); //reset selected bin/s
               
                ////BinSelected get from ivntytransferviewmodel.invtytransferline.BinAllocation
                var binLine = _model.inventoryTransfer.InventoryTransferLines.AsEnumerable().Where(x => x.LineNum == _curLine)
                                     .Select(x => x.StockTransferLinesBinAllocations.Where(sel=> sel.BatchNumber == distNumber));

                _binSelected = new();
                foreach (var bins in binLine)
                {
                    foreach (var bin in bins)
                    {
                        _binSelected.Add(new BinViewModel.BinMappingViewModel.BinAccumulator
                        {
                            BinAbs = bin.BinAbsEntry,
                            //BinCode = filteredBinList.Where(sel => sel.BinAbs == bin.BinAbsEntry).Select(sel => sel.BinCode).FirstOrDefault(),
                            OnHandQty = bin.Quantity,
                        });
                    }
                };

                filteredBinDestList = await _restService.Get<List<vm.BinViewModel.BinMappingViewModel.BinAccumulator>>($@"BatchSerial/GetBinByLoc?location={_model.inventoryTransfer.DestLocation}");
                filteredBinDestList.ForEach(fe =>
                {
                    fe.CheckState = _binSelected == null ? false : _binSelected.Where(sel => sel.BinAbs == fe.BinAbs).Any();
                    fe.OnHandQty = fe.CheckState ? 1 : 0;
                });
                //if (gridBin != null) await gridBin.RefreshDataAsync();
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
            _showMode = 3;             
            _model.IsBusy = false;
        }
        public void CheckAllBin(bool args)
        {
            filteredBinList.ForEach(fe => fe.CheckState = args);
        }
        public void CheckAllDestBin(bool args)
        {
            filteredBinDestList.ForEach(fe => fe.CheckState = args);
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
                var _data = _mapper.Map<InventoryTransferModel>(_model.inventoryTransfer);
                var palletcode = _data.InventoryTransferLines.Select(x => x.PalletCode).FirstOrDefault();
                var data = await _restService.ExecutePostAsync<DataManager.Models.InventoryTransfer.InventoryTransferModel>("InventoryTransfer", _data);
                if (data.IsSuccessful)
                {

					_hubConnection.InvokeAsync("UpdateSalesOrder", _model.inventoryTransfer.InventoryTransferLines.FirstOrDefault().SalesOrderNum);

					//wait _js.InvokeVoidAsync("toastr.success", "Posted Successfully");                
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
                    var inn = data.ErrorMessage.IndexOf($@"""message"" : """);
                    string eRms = data.ErrorMessage.Substring( inn, data.ErrorMessage.Length - 1 - inn);
                    var strSplit = string.IsNullOrEmpty(eRms) ? SplitToArr(data.ErrorMessage) : SplitToArr(eRms);

					//_blazorStrap.Toaster.Add("Error on posting", strSplit.Length > 0 ? strSplit[3] : data.ErrorMessage, o =>
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

        private async Task OnPalletBarcodeEnter(KeyboardEventArgs e)
        {

            //bool enterIsPressed = e.Code == "Enter" || e.Code == "NumpadEnter";
            //if (!enterIsPressed) return;
            if (e.Code == "Enter" || e.Code == "NumpadEnter" || e.Key == "Enter")
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
                    return;
                }

                if (!string.IsNullOrEmpty(_model.PalletBarcode))
                {
                    _model.IsBusy = true;
                    await FetchPalletBinData();
                    if (!string.IsNullOrEmpty(_lrefBtnBin.Id))
                        await _js.InvokeVoidAsync("FocusByElemRef", _lrefBtnBin);
                    _model.IsBusy = false;
                }
                StateHasChanged();
            }
        }

        public async Task FetchPalletBinData()
        {
            try
            {
                _model.IsBusy = true;
                string barcode = _model.PalletBarcode;
                var pallet = await _restService.Get<Models.Pallet>($"Pallets/{barcode}");
                if (pallet != null)
                {
                    Func<string, string> encode = e => HttpUtility.UrlEncode(e);
                    ////VALIDATION FROM UDO MATRIX
                    var _palletMatrix = await _restService.Get<Models.Pallet>($"InventoryTransfer/GetPalletMatrix?itemCode={encode(pallet.ItemCode)}&palletCode={encode(pallet.Code)}&serviceType={encode(pallet.ServiceType)}&isManualTransfer={true}&binCode={encode(pallet.BinCode)}");
                    _model.BinBarcode = pallet.BinCode;
                    _model.inventoryTransfer.ServiceType = pallet.ServiceType;
                    _model.inventoryTransfer.UTransferType = _palletMatrix.TransferType;
                    _model.inventoryTransfer.DisplayStatus = _palletMatrix.DisplayStatus;
                    _model.inventoryTransfer.SortCodeStatus = _palletMatrix.SortCodeStatus;
                    _model.inventoryTransfer.InventoryTransferLines = new List<InventoryTransferLine>(); // TO DISABLE MULTIPLE PALLET
                    var cnt = _model.inventoryTransfer.InventoryTransferLines.Count();
                    _model.inventoryTransfer.InventoryTransferLines.Add(new InventoryTransferLine
                    {
                        LineNum = cnt,
                        PalletCode = pallet.Code,
                        BinCode = (_model.inventoryTransfer.SortCodeStatus != 3 && _model.inventoryTransfer.SortCodeStatus != 4 && _model.inventoryTransfer.SortCodeStatus != 5) ? pallet.BinCode : "", //CLEAR BIN IF THE SORT ORDER IS 3,4, or 5 -KARL 12/13/2023
						ActualBoxQty = 0, //Because this is manual //pallet.ActualQuantity,
                        PlannedBoxQty = pallet.PlannedQuantity,
                        SalesOrderNum = pallet.SalesOrderDocNum,
                        ItemCode = pallet.ItemCode,
                        ItemName = pallet.ItemName,
                        FromWarehousecode = _palletMatrix != null ? _palletMatrix.FromWarehouseCode : "",
                        Warehousecode = _palletMatrix != null ? _palletMatrix.WarehouseCode : "",
                    });
                    _model.inventoryTransfer.LocationCode = _model.inventoryTransfer.InventoryTransferLines.Select(x => x.FromWarehousecode).FirstOrDefault();
                    _model.inventoryTransfer.DestLocation = _model.inventoryTransfer.InventoryTransferLines.Select(x => x.Warehousecode).FirstOrDefault();

					//REMOVE VALIDATION OF BIN IF THE SORT ORDER IS 3 OR 4 -KARL 12/13/2023
					//INCLUDE SORT ORDER 6 - KARL 07/02/2024
					if (_model.inventoryTransfer.SortCodeStatus == 2 || _model.inventoryTransfer.SortCodeStatus == 3 || _model.inventoryTransfer.SortCodeStatus == 4 || _model.inventoryTransfer.SortCodeStatus == 6)
					{
						_isNoBin = true;
						_isDisabled = false;
					}

					await _js.InvokeVoidAsync("FocusByElemRef", _lrefBtnPallet);
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
            _model.IsBusy = false;
            _itemCode = _model.ItemCode;
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
        private async Task ChangeTransferType()
        {
            //// Check the display status from the Matrix
            //// If TransferType not from the list then 
            ////    Display Status: For Storage - Receiving && SortCode: 0
            //// Else if TransferType is Non-Conformity
            ////    Display Status: Non-Conformity && SortCode: -1
            //// else
            ////    Display Status & SortCode: From Matrix
            try
            {
                _model.IsBusy = true;
                Func<string, string> encode = e => HttpUtility.UrlEncode(e);
                var _palletMatrix = await _restService.Get<Dictionary<string, string>>($"InventoryTransfer/GetTransferTypeFromMatrix?transferType={encode(_model.inventoryTransfer.UTransferType)}&serviceType={encode(_model.inventoryTransfer.ServiceType)}");
                _model.inventoryTransfer.DisplayStatus = _palletMatrix.Where(x=> x.Key == "DisplayStatus").Select(x=> x.Value).FirstOrDefault();
                var _sortCode = _palletMatrix.Where(x => x.Key == "SortCode").Select(x => x.Value).FirstOrDefault();
                _model.inventoryTransfer.SortCodeStatus = Convert.ToInt32(_sortCode);

                //IF THE TRANSFER TYPE AND DESTINATION IS NON CONFORMITY, DISABLE BIN THEN ENABLE SAVE -KARL 12/20/2023
                if (_model.inventoryTransfer.SortCodeStatus == -1 && _model.inventoryTransfer.DestLocation == "NC")
                {
                    _isDisabled = false;
                    _isNoBin = true;
                }
            }
            catch( Exception ex )
            {              
                var strSplit = SplitToArr(ex.Message);
                _blazorStrap.Toaster.Add("Info", strSplit.Length > 0 ? strSplit[1] : ex.Message, o =>
                {
                    o.Color = BSColor.Danger;
                    o.CloseAfter = 3000;
                    o.Toast = Toast.TopRight;
                });
            }
            _model.IsBusy = false;
        }
        private void ChangeWhsCodeSource()
        {
            foreach (var line in _model.inventoryTransfer.InventoryTransferLines)
            {
                line.FromWarehousecode = _model.inventoryTransfer.LocationCode;
            }
        }
        private void ChangeWhsCodeDestination()
        {
            foreach(var line in _model.inventoryTransfer.InventoryTransferLines)
            {
                line.Warehousecode = _model.inventoryTransfer.DestLocation;
            }

            //IF THE TRANSFER TYPE AND DESTINATION IS NON CONFORMITY, DISABLE BIN THEN ENABLE SAVE -KARL 12/20/2023
            if (_model.inventoryTransfer.SortCodeStatus == -1 && _model.inventoryTransfer.DestLocation == "NC")
            {
                _isDisabled = false;
                _isNoBin = true;
            }
        }
        private async Task Clear()
        {
            _model.inventoryTransfer = new();
            _model.inventoryTransfer.DocDate = DateTime.Now;
        }
        private void ToggleDefault()
        {
            _showMode = 0;
        }
        private void ShowMode(int val)
        {
            _showMode = val;
        }
        private void AllocateBatchSrcList()
        {
            _model.IsBusy = true;            
            ////ASSIGN THE SELECTED BATCH TO THE BINDED MODEL
            foreach(var line in _model.inventoryTransfer.InventoryTransferLines)
            { 
                if (line.LineNum == _curLine)
                {
                    line.ActualBoxQty = decimal.ToInt32(gridBatch.View.Where(sel=> sel.CheckState == true).Select(sel => sel.Quantity).Sum() ?? 0);

                    ////REFRESH DATA
                    line.BatchNumbers = line.BatchNumbers.Where(sel => sel.BaseLineNumber != _curLine).ToList();
                    line.BatchNumbers.AddRange(
                                            gridBatch.View.Where(x => x.CheckState == true)
                                                        .Select(x => new InventoryTransferBatch { 
                                                            BatchNumber = x.DistNumber, 
                                                            Quantity = decimal.ToInt32(x.Quantity??0), 
                                                            ManufacturerSerialNumber = x.MnfSerial,
                                                            InternalSerialNumber = x.LotNumber,
                                                        }).AsList());

                    line.StockTransferLinesBinAllocations = new List<DataManager.Models.Bins.BinAllocation>();

                    ////REFRESH DATA : Remove only the Bin Assigned for each Line of Batches
                    //line.StockTransferLinesBinAllocations = line.StockTransferLinesBinAllocations
                    //                                        .Where(x => x.BaseLineNumber == _curLine 
                    //                                                && x.BinActionType != "batFromWarehouse")
                    //                                        .ToList();

                    string actionType = _model.inventoryTransfer.LocationCode.ToLower().Contains("nc") ? "batToWarehouse" : "batFromWarehouse";

					////ADD THE CURRENT SELECTED LIST
					line.StockTransferLinesBinAllocations.AddRange(
                                            gridBatch.View.Where(x => x.CheckState == true)
                                                            .Select(x => new DataManager.Models.Bins.BinAllocation
                                                            {
                                                                BatchNumber = x.DistNumber,
                                                                BinAbsEntry = x.BinAbs,
                                                                Quantity = x.Quantity??0,
                                                                SerialAndBatchNumbersBaseLine = x.BaseLineNumber,
                                                                BinActionType = actionType,
                                                            }).AsList());
                    _showMode = 0;
                    _model.IsBusy = false;
                    return;
                }
            };
        }

        private void AllocateBinSrcList()
        {
            _model.IsBusy = true;            
            
            ////ASSIGN THE SELECTED BATCH TO THE BINDED MODEL
            foreach (var line in _model.inventoryTransfer.InventoryTransferLines)
            {
                if (line.LineNum == _curLine)
                {
                    ////REFRESH DATA : Remove only the current Bin Assigned to the current line & current Batch
                    line.StockTransferLinesBinAllocations = line.StockTransferLinesBinAllocations
                                                    .Where(x => x.BaseLineNumber == _curLine 
                                                        && x.BatchNumber != _curBatch)
                                                    .ToList();                    
                    
                    ////ADD THE CURRENT SELECTED LIST
                    line.StockTransferLinesBinAllocations.AddRange(
                                        gridBin.View.Where(x => x.CheckState == true)
                                                    .Select(x => new DataManager.Models.Bins.BinAllocation
                                                    {
                                                        BatchNumber = _curBatch,
                                                        BinAbsEntry = x.BinAbs,
                                                        Quantity = x.OnHandQty?? 0,
                                                        BaseLineNumber = x.BaseLineNumber,
                                                        BinActionType = "batToWarehouse",
                                                    }).ToList());    
                    ////Auto-mark as Check the batch
                    filteredBatchList.Where(sel=> sel.DistNumber == _curBatch)
                                    .ToList()
                                    .ForEach(fe => 
                                    {
                                        fe.CheckState = true;
                                    });
                    _showMode = 3; //Allocate Bin Destination List
                    _model.IsBusy = false;
                    return;
                }
            };
        }

        private void AllocateBinDesList()
        {
            _model.IsBusy = true;

            ////ASSIGN THE SELECTED BATCH TO THE BINDED MODEL
            foreach (var line in _model.inventoryTransfer.InventoryTransferLines)
            {
                if (line.LineNum == _curLine)
                {
                    ////REFRESH DATA : Remove only the current Bin Assigned to the current line & current Batch
                    line.StockTransferLinesBinAllocations = line.StockTransferLinesBinAllocations
                                                    .Where(x => x.BaseLineNumber == _curLine
                                                        && x.BatchNumber != _curBatch)
                                                    .ToList();

                    ////ADD THE CURRENT SELECTED LIST
                    line.StockTransferLinesBinAllocations.AddRange(
                                        gridBinDest.View.Where(x => x.CheckState == true)
                                                    .Select(x => new DataManager.Models.Bins.BinAllocation
                                                    {
                                                        BatchNumber = _curBatch,
                                                        BinAbsEntry = x.BinAbs,
                                                        Quantity = x.OnHandQty ?? 0,
                                                        BaseLineNumber = x.BaseLineNumber,
                                                        BinActionType = "batToWarehouse",
                                                    }).ToList());
                    ////Auto-mark as Check the batch
                    filteredBatchList.Where(sel => sel.DistNumber == _curBatch)
                                    .ToList()
                                    .ForEach(fe =>
                                    {
                                        fe.CheckState = true;
                                    });
                    _showMode = 1; //back to Batch list
                    _model.IsBusy = false;
                    return;
                }
            };
        }

        private void FilterList(ChangeEventArgs e)
        {
            SearchList(e.Value.ToString() ?? "");
        }
        private void SearchList(string value)
        {
            value = value.Trim();
            filteredBatchList = _model.batchList.Where(x =>
            {
                var include =
                x.DistNumber.Contains(value)
                || x.MnfSerial.Contains(value)
                || x.LotNumber.ToString().Contains(value);
                return include;
            }).ToList();
        }
        public string[] SplitToArr(string str)
        {
            string[] strArr = str.Split(new string[] { $@"""" }, StringSplitOptions.None);
            return strArr;
        }
        private void ScanResult(string data)
        {
            //BarCode = e;
            try
            {
                if (_scanType == "Pallet")
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
    }
}
