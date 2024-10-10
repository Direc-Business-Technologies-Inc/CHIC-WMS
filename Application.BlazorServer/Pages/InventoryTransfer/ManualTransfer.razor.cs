using Application.BlazorServer.Extensions;
using Application.Libraries.SAP;
using Application.Models;
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

namespace Application.BlazorServer.Pages.InventoryTransfer
{
    public partial class ManualTransfer : ComponentBase
    {
        [Inject] private IJSRuntime _js { get; set; }
        [Inject] private IInventoryTransferService _invTransferService { get; set; }
        [Inject] private IWarehouseService _warehouseService { get; set; }
        [Inject] private IDbContextFactory<SapDb> _sapDbFactory { get; set; }
        [Inject] private IReceivingService _receivingService { get; set; }
        [Inject] private IPalletService _palletService { get; set; }
        [Inject] private IBatchSerialService _batchSerialService {  get; set; }
        [Inject] private ISalesOrderService _salesOrderService { get; set; }
        [Inject] private IMapper _mapper { get; set; }
        [Inject] protected IBlazorStrap _blazorStrap { get; set; } = default!;

        private InventoryTransferViewModel _model;
        private List<vm.BatchSerialViewModel.BatchSerial> filteredBatchList { get; set; }
        private List<vm.BinViewModel.BinMappingViewModel.BinAccumulator> filteredBinList { get; set; }
        private List<vm.BinViewModel.BinMappingViewModel.BinAccumulator> filteredBinDestList { get; set; }
        private List<vm.BatchSerialViewModel.BatchSerial> _batchSelectedSrc { get; set; } = new List<vm.BatchSerialViewModel.BatchSerial>();
        private List<vm.BinViewModel.BinMappingViewModel.BinAccumulator> _binSelected { get; set; } = new List<vm.BinViewModel.BinMappingViewModel.BinAccumulator>();

        private Dictionary<string, string> _UTransferType { get; set; }
        private Dictionary<string, string> _filteredLocation = new();
        private string _palletCode { get; set; } = "";
        private string _binCode { get; set; } = "";
        private bool _isDisabled { get; set; } = true;
        private bool _isNoBin { get; set; } = false;
        private int _curLine { get; set; }
        private string _curBatch { get; set; }
        private string _curBin { get; set; }
        private bool isGranted = false;
        private int _showMode { get; set; } = 0;

        private vm.InventoryTransferLine? LineSelected { get; set; }

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

                filteredBatchList = await _batchSerialService.GetBatchSerialByMnfSerialLoc(lineSel.ItemCode, lineSel.PalletCode, lineSel.FromWarehousecode);
                filteredBatchList.ForEach(fe =>
                {
                    fe.Quantity = Math.Round(fe.Quantity ?? 0);
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

        private async Task FetchBinList(string itemCode, string distNumber, string whsCode, int baseLine)
        {
            try
            {
                _curBatch = distNumber;
                if (_binSelected != null) _binSelected.Clear(); //reset selected bin/s

                ////BinSelected get from ivntytransferviewmodel.invtytransferline.BinAllocation
                var binLine = _model.inventoryTransfer.InventoryTransferLines.AsEnumerable().Where(x => x.LineNum == _curLine)
                                     .Select(x => x.StockTransferLinesBinAllocations.Where(sel => sel.BatchNumber == distNumber));

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
                
                filteredBinList = await _batchSerialService.GetBinByLoc(_model.inventoryTransfer.DestLocation);
                filteredBinList.ForEach(fe =>
                {
                    fe.OnHandQty = Math.Round(fe.OnHandQty ?? 0);
                    fe.CheckState = _binSelected == null ? false : _binSelected.Where(sel => sel.BinAbs == fe.BinAbs).Any();
                });
                //if (gridBin != null) await gridBin.RefreshDataAsync();
                _showMode = 2;
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

        private async Task FetchBinDestList(string itemCode, string distNumber, string whsCode, int baseLine)
        {
            try
            {
                _curBatch = distNumber;
                _model.IsBusy = true;
                if (_binSelected != null) _binSelected.Clear(); //reset selected bin/s

                ////BinSelected get from ivntytransferviewmodel.invtytransferline.BinAllocation
                var binLine = _model.inventoryTransfer.InventoryTransferLines.AsEnumerable().Where(x => x.LineNum == _curLine)
                                     .Select(x => x.StockTransferLinesBinAllocations.Where(sel => sel.BatchNumber == distNumber));

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

                filteredBinDestList = await _batchSerialService.GetBinByLoc(_model.inventoryTransfer.DestLocation);
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
                filteredBinDestList.Where(sel => sel.BinCode == binCode).ToList().ForEach(fe =>
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
                _blazorStrap.Toaster.Add("Error", "Error on posting", o =>
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
                    var _palletMatrix = _invTransferService.GetPalletMatrix(pallet.ItemCode, pallet.Code, pallet.BinCode, pallet.ServiceType, true);
                    _model.BinBarcode = pallet.BinCode;
                    _model.inventoryTransfer.ServiceType = _palletMatrix.ServiceType;
                    _model.inventoryTransfer.UTransferType = _palletMatrix.TransferType;
                    _model.inventoryTransfer.DisplayStatus = _palletMatrix.DisplayStatus;
                    _model.inventoryTransfer.SortCodeStatus = _palletMatrix.SortCodeStatus;
                    _model.inventoryTransfer.InventoryTransferLines = new List<vm.InventoryTransferLine>(); // TO DISABLE MULTIPLE PALLET
                    _model.inventoryTransfer.InventoryTransferLines.Add(new vm.InventoryTransferLine
                    {
                        PalletCode = pallet.Code,
                        BinCode = (_model.inventoryTransfer.SortCodeStatus != 3 && _model.inventoryTransfer.SortCodeStatus != 4 && _model.inventoryTransfer.SortCodeStatus != 5) ? pallet.BinCode : "", //CLEAR BIN IF THE SORT ORDER IS 3,4, or 5 -KARL 12/13/2023
                        ActualBoxQty = 0, //pallet.ActualQuantity,
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
                var _palletMatrix = await _invTransferService.GetTransferTypeFromMatrix(encode(_model.inventoryTransfer.UTransferType),encode(_model.inventoryTransfer.ServiceType));
                _model.inventoryTransfer.DisplayStatus = _palletMatrix.Where(x => x.Key == "DisplayStatus").Select(x => x.Value).FirstOrDefault();
                var _sortCode = _palletMatrix.Where(x => x.Key == "SortCode").Select(x => x.Value).FirstOrDefault();
                _model.inventoryTransfer.SortCodeStatus = Convert.ToInt32(_sortCode);

                //IF THE TRANSFER TYPE AND DESTINATION IS NON CONFORMITY, DISABLE BIN THEN ENABLE SAVE -KARL 12/20/2023
                if (_model.inventoryTransfer.SortCodeStatus == -1 && _model.inventoryTransfer.DestLocation == "NC")
                {
                            _isDisabled = false;
                            _isNoBin = true;
                }
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
            foreach (var line in _model.inventoryTransfer.InventoryTransferLines)
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
            _isDisabled = true;
        }
        private void ShowMode(int val)
        {
            _showMode = val;
        }
        private void AllocateBatchSrcList()
        {
            _showMode = 0;

            ////ASSIGN THE SELECTED BATCH TO THE BINDED MODEL
            foreach (var line in _model.inventoryTransfer.InventoryTransferLines)
            {
                if (line.LineNum == _curLine)
                {
                    line.ActualBoxQty = decimal.ToInt32(gridBatch.View.Where(sel => sel.CheckState == true).Select(sel => sel.Quantity).Sum() ?? 0);

                    ////REFRESH DATA
                    line.BatchNumbers = line.BatchNumbers.Where(sel => sel.BaseLineNumber != _curLine).ToList();
                    line.BatchNumbers.AddRange(
                                            gridBatch.View.Where(x => x.CheckState == true)
                                                        .Select(x => new InventoryTransferBatch
                                                        {
                                                            BatchNumber = x.DistNumber,
                                                            Quantity = decimal.ToInt32(x.Quantity ?? 0),
                                                            ManufacturerSerialNumber = x.MnfSerial,
                                                            InternalSerialNumber = x.LotNumber,
                                                        }).ToList());

                    line.StockTransferLinesBinAllocations = new List<DataManager.Models.Bins.BinAllocation>();

                    //////REFRESH DATA : Remove only the Bin Assigned for each Line of Batches
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
                                                                Quantity = x.Quantity ?? 0,
                                                                SerialAndBatchNumbersBaseLine = x.BaseLineNumber,
                                                                BinActionType = actionType,
                                                            }).ToList());
                    return;
                }
            };
        }

        private void AllocateBinSrcList()
        {
            _showMode = 1; //Back to Batch List

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

    }
}
