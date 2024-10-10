using Application.Libraries.SAP;
using Application.Libraries.SAP.ServiceLayer;
using Application.Libraries.SAP.SL;
using Application.Models;
using B1SLayer;
using DataManager.Libraries.Repositories;
using DataManager.Models.Enums;
//using Application.Services.Repositories;
using DataManager.Models.InventoryTransfer;
using DataManager.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using System.Runtime.InteropServices;

namespace Application.Services.Core
{
	internal class InventoryTransferService : IInventoryTransferService
	{
		private readonly IDbContextFactory<SapDb> _sapDbFactory;
		private readonly IReceivingService _receivingService;
		private readonly ISalesOrderService _salesOrderService;
		private readonly IServiceLayerDataAccess _sl;
		private readonly IPalletService _palletService;
		private readonly IUtilities _utilities;
		private readonly IConfiguration _conf;
		private readonly IMySqlDataAccess _mysql;
		private readonly IBinDataServices _binDataService;
		private readonly Context _addonDb;

		public InventoryTransferService(IDbContextFactory<SapDb> sapDbFactory, IReceivingService receivingService, Context addonDb, ISalesOrderService salesOrderService, IPalletService palletService, IServiceLayerDataAccess sl, IUtilities utilities, IConfiguration conf, IMySqlDataAccess mysql, IBinDataServices binDataService)
		{
			_sapDbFactory = sapDbFactory;
			_receivingService = receivingService;
			_addonDb = addonDb;
			_salesOrderService = salesOrderService;
			_palletService = palletService;
			_sl = sl;
			_utilities = utilities;
			_conf = conf;
			_mysql = mysql;
			_binDataService = binDataService;
		}

		public InventoryTransferModel Create(InventoryTransferModel data)
		{
			throw new NotImplementedException();
		}

		private string Serialize(object data) => JsonConvert.SerializeObject(data, ServiceLayerExtensions.SerializerSettings);

		private async Task PostForStorage(InventoryTransferModel data)
		{
			// TODO: Update Batches' status to "locked" if it's a non-conformity
			if (data.DocDate.Year < 2000) data.DocDate = DateTime.Now;
			var payload = new StockTransfer
			{
				DocDate = data.DocDate,
				U_Remarks = _utilities.GetPostRemarks()
			};

			string sourceWarehouse = data.LocationCode;
			if (string.IsNullOrEmpty(sourceWarehouse)) throw new Exception("Warehouse should not be null");
			if (!(sourceWarehouse == "RC" || sourceWarehouse == "U")) throw new Exception("For storage pallets should only come from receiving or unloading");

			List<Pallet> toBePostedPallets = new List<Pallet>();

			bool isFromUnloading = sourceWarehouse == "U";
			foreach (var pallet in from line in data.InventoryTransferLines
								   let palletCode = line.PalletCode
								   let pallet = _palletService.Get(palletCode)
								   select pallet//pallet.Boxes
			)
			{
				toBePostedPallets.Add(pallet);
				var stLine = new StockTransferLine()
				{
					ItemCode = pallet.ItemCode,
					FromWarehouseCode = sourceWarehouse,
					WarehouseCode = pallet.WarehouseCode
				};
				foreach (var box in pallet.Boxes)
				{
					if (box.Status is null) continue;
					var batch = new BatchNumber
					{
						BatchNumberProperty = box.Id,
						Quantity = 1
					};

					stLine.BatchNumbers.Add(batch);

					var binAlloc = new StockTransferLinesBinAllocation
					{
						BinAbsEntry = pallet.BinEntry,
						Quantity = 1,
						BinActionType = BinActionTypeEnum.BatToWarehouse,
						SerialAndBatchNumbersBaseLine = stLine.BatchNumbers.IndexOf(batch),
					};

					stLine.StockTransferLinesBinAllocations.Add(binAlloc);
				}

				stLine.Quantity = stLine.BatchNumbers.Sum(x => x.Quantity);
				payload.StockTransferLines.Add(stLine);
			}

			var docnums = toBePostedPallets.Select(x => x.SalesOrderDocNum).Distinct();

			var emee = Serialize(payload);

			var toBePostedPalletCodes = toBePostedPallets.Select(x => x.Code).Distinct();
			List<int> completedTransfers = new();

			foreach (var docnum in docnums)
			{
				var soPallet = _salesOrderService.GetPallets(docnum).Where(x => !toBePostedPalletCodes.Contains(x.Code)).ToList();
				bool noPalletsToBeReceived;
				using (var db = _sapDbFactory.CreateDbContext())
				{
					var receivedPallets = db.OBTN.Where(x => !string.IsNullOrEmpty(x.MnfSerial) && x.U_SONo == docnum).Select(x => x.MnfSerial).ToList();
					noPalletsToBeReceived = soPallet.TrueForAll(x => receivedPallets.Contains(x.Code));
				}
				if (noPalletsToBeReceived)
					completedTransfers.Add(docnum);
			}

			try
			{
				var requests = new List<SLBatchRequest>();
				SLBatchRequest req1 = new(HttpMethod.Post, "StockTransfers", Serialize(payload), contentID: 1);


				requests.Add(req1);
				using (var db = _sapDbFactory.CreateDbContext())
				{

					#region Create request for updating batch status
					// TODO: Update payload's batches status if say so
					//foreach(var batch in payload.StockTransferLines.SelectMany(x=>x.BatchNumbers))
					//{

					//    var batchPayload = new BatchNumberDetail
					//    {
					//        U_BatchStatus = Status.<Choose status>.GetDescription()
					//    };
					//    SLBatchRequest batchStatusUpdate = new(HttpMethod.Patch, $"BatchNumberDetails('{batch.BatchNumberProperty}')", serialize(batchPayload), contentID: requests.Count + 1);
					//}
					#endregion

					Dictionary<int, (int DocEntry, string U_ServiceType)> serviceMap = new();
					#region Create completed inventory transfers batch's status update requests
					foreach (var batch in payload.StockTransferLines.SelectMany(x => x.BatchNumbers))
					{
						var baseBatch = (from t0 in db.OBTN where t0.DistNumber == batch.BatchNumberProperty select t0).Single();
						var docnum = baseBatch.U_SONo.Value;

						(int DocEntry, string U_ServiceType) serviceSource;
						if (!serviceMap.TryGetValue(docnum, out serviceSource))
						{
							var orderData = (from t0 in db.ORDR
											 where t0.DocNum == docnum
											 select new { t0.DocEntry, t0.U_ServiceType }).Single();

							var tuple = (orderData.DocEntry, orderData.U_ServiceType);
							serviceMap.Add(docnum, tuple);
							serviceSource = tuple;
						}

						var newStatus = serviceSource.U_ServiceType.Trim().Equals(ServiceType.StorageOnly.GetDescription()) ? Status.InStorage : Status.InStorage_ForIrradiation;

						if (isFromUnloading)
						{
							newStatus = Status.Irradiated_InStorage_ForQA;
						}

						var batchPayload = new BatchNumberDetail
						{
							U_BatchStatus = newStatus.GetDescription()
						};


						if (sourceWarehouse == "RC" && data.TransferType == TransferTypeEnum.ForStorage)
						{
							// update date and time if from receiving to storage which is bin
							batchPayload.U_TIMS_ITRecBin = DateTime.Now;
							batchPayload.U_TIMS_ITRecBinTime = TimeOfDay.Now;
						}

						if (sourceWarehouse == "U" && data.TransferType == TransferTypeEnum.ForStorage)
						{
							// update date and time if from unloading bay to storage which is bin
							batchPayload.U_TIMS_IUBBin = DateTime.Now;
							batchPayload.U_TIMS_IUBBinTime = TimeOfDay.Now;

						}

						SLBatchRequest batchStatusUpdate = new(HttpMethod.Patch, $"BatchNumberDetails({baseBatch.AbsEntry})", Serialize(batchPayload), contentID: requests.Count + 1);
						requests.Add(batchStatusUpdate);
					}
					#endregion
				}
				var resps = await _sl.BatchAsync(requests.ToArray());
				var errors = resps.Where(x => !x.IsSuccessStatusCode);
				if (errors.Any())
				{
					var resp = errors.First();
					var error = await resp.Content.ReadAsStringAsync();
					resp.EnsureSuccessStatusCode();
				}


				//KASO - 2023/10/22 - TEMPORARILY REMOVED AS PER S.A. NICS
				//using (var addonDb = _addonDb.Database.BeginTransaction())
				//{
				//    try
				//    {
				//        foreach (var pallet in data.InventoryTransferLines)
				//        {
				//            var basePallet = _palletService.Get(pallet.PalletCode);
				//            var palletCode = pallet.PalletCode;
				//            var newBinCode = pallet.BinCode;
				//            var newBin = _addonDb.BinAssignments.Where(x => x.BinCode == newBinCode).SingleOrDefault();

				//            var palletEntity = _addonDb.BinAssignments.Where(x => x.PalletNo == palletCode).Single();
				//            palletEntity.PalletNo = "";

				//            if (newBin is null)
				//            {
				//                newBin = new BinAssignment
				//                {
				//                    BinCode = newBinCode,
				//                    CreateDate = DateTime.Now,
				//                    PalletNo = palletCode,
				//                    SONo = basePallet.SalesOrderDocNum.ToString()
				//                };
				//                _addonDb.BinAssignments.Add(newBin);
				//                _addonDb.SaveChanges();
				//            }

				//            _addonDb.SaveChanges();
				//        }
				//        addonDb.Commit();
				//    } catch(Exception ex)
				//    {
				//        addonDb.Rollback();
				//    }
				//}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<InventoryTransferModel> CreateAsync(InventoryTransferModel data)
		{
			// TODO: Implement SAP service layer posting of inventory transfer logic
			//switch (data.TransferType)
			//{
			//    case TransferTypeEnum.ForStorage:
			//        await PostForStorage(data); break;
			//    case TransferTypeEnum.ForIrradiation:
			//        await PostForIrradiation(data); break;
			//    case TransferTypeEnum.ForRelease:
			//        await PostForRelease(data); break;
			//    case TransferTypeEnum.AtIrradiation:
			//        await PostAtIrradiation(data); break;
			//}
			await PostInventory(data);
			return new();
		}
		private async Task PostInventory(InventoryTransferModel data)
		{
			try
			{
				using (var db = _sapDbFactory.CreateDbContext())
				{


					if (data.DocDate.Year < 2000) data.DocDate = DateTime.Now;
					var payload = new StockTransfer
					{
						DocDate = data.DocDate,
						U_Remarks = _utilities.GetPostRemarks(),
						U_TransferType = data.UTransferType,
					};

					//List<Pallet> toBePostedPallets = new List<Pallet>();

					var serviceData = (from t0 in db.SERVICE_DATA_ROW
									   where t0.U_TransferType == data.UTransferType
									   select t0).FirstOrDefault();

					foreach (var line in data.InventoryTransferLines)
					{
						//var palletCode = line.PalletCode;
						Pallet pallet = _palletService.Get(line.PalletCode);
						//toBePostedPallets.Add(pallet);

						var stLine = new StockTransferLine()
						{
							ItemCode = line.ItemCode,
							FromWarehouseCode = line.FromWarehouseCode,
							WarehouseCode = line.WarehouseCode,
						};

						string ItemCode = data.InventoryTransferLines.FirstOrDefault().ItemCode;

						if (line.BatchNumbers.Count > 0) ////FOR MANUAL SELECTION OF BATCH
						{
							foreach (var btn in line.BatchNumbers)
							{
								if (string.IsNullOrEmpty(btn.BatchNumber)) continue;
								var batch = new BatchNumber
								{
									BatchNumberProperty = btn.BatchNumber,
									Quantity = btn.Quantity,
									ManufacturerSerialNumber = btn.ManufacturerSerialNumber,
									BaseLineNumber = btn.BaseLineNumber,
									ItemCode = ItemCode
								};
								stLine.BatchNumbers.Add(batch);

								////FROM SOURCE
								var obinModel = db.OBIN.Where(x => x.BinCode == line.BinCode).SingleOrDefault();
								int binEntry = (serviceData?.U_SortCode ?? 0) == 5 ? (obinModel.AbsEntry) : pallet.BinEntry;
								bool binFromEnable = false;
								binFromEnable = (from t0 in db.OWHS where t0.BinActivat == "Y" && t0.WhsCode == line.FromWarehouseCode select t0.WhsCode).Any();
								if (binFromEnable)
								{
									foreach (var bin in line.StockTransferLinesBinAllocations.Where(sel => sel.BinActionType == "batFromWarehouse" && sel.BatchNumber == btn.BatchNumber && sel.BaseLineNumber == btn.BaseLineNumber))
									{
										var binAlloc = new StockTransferLinesBinAllocation
										{
											BinAbsEntry = binEntry,
											Quantity = decimal.ToDouble(bin.Quantity),
											BinActionType = BinActionTypeEnum.BatFromWarehouse,
											SerialAndBatchNumbersBaseLine = stLine.BatchNumbers.IndexOf(batch),
										};
										stLine.StockTransferLinesBinAllocations.Add(binAlloc);
									}
								}

								//// NOT POSSIBLE BOTH LOCATION ARE BIN ENABLED : Need for enhancement                                
								//// TO DESTINATION
								bool binToEnable = false;
								binToEnable = (from t0 in db.OWHS where t0.BinActivat == "Y" && t0.WhsCode == line.WarehouseCode select t0.WhsCode).Any();
								if (binToEnable)
								{
									foreach (var bin in line.StockTransferLinesBinAllocations.Where(sel => sel.BinActionType == "batToWarehouse" && sel.BatchNumber == btn.BatchNumber && sel.BaseLineNumber == btn.BaseLineNumber))
									{
										var binAlloc = new StockTransferLinesBinAllocation
										{
											BinAbsEntry = binEntry,
											Quantity = decimal.ToDouble(bin.Quantity),
											BinActionType = BinActionTypeEnum.BatToWarehouse,
											SerialAndBatchNumbersBaseLine = stLine.BatchNumbers.IndexOf(batch)
										};
										stLine.StockTransferLinesBinAllocations.Add(binAlloc);
									}
								}
							} //End of BatchNumber
							stLine.Quantity = stLine.BatchNumbers.Sum(x => x.Quantity);
						}
						#region MATRIX_AUTO_SELECT_BATCH
						else ////FROM MATRIX AUTO SELECT
						{
							foreach (var box in pallet.Boxes)
							{
								//if (box.Status is null) continue; REMOVED KASI WALA NANG HUHUGUTAN FROM RECEIVING -08/06/2024
								var batch = new BatchNumber
								{
									BatchNumberProperty = box.Id,
									Quantity = 1,
								};
								stLine.BatchNumbers.Add(batch);

								#region CHECK_IF_WHSE_BIN_ENABLE                            
								//// NOT POSSIBLE BOTH LOCATION ARE BIN ENABLED : Need for enhancement

								var obinModel = db.OBIN.Where(x => x.BinCode == line.BinCode).SingleOrDefault();
								int binEntry = (serviceData?.U_SortCode ?? 0) == 5 ? (obinModel.AbsEntry) : pallet.BinEntry;
								bool binFromEnable = false;
								binFromEnable = (from t0 in db.OWHS where t0.BinActivat == "Y" && t0.WhsCode == line.FromWarehouseCode select t0.WhsCode).Any();
								if (binFromEnable)
								{
									var binAlloc = new StockTransferLinesBinAllocation
									{
										BinAbsEntry = binEntry,
										Quantity = 1,
										BinActionType = BinActionTypeEnum.BatFromWarehouse,
										SerialAndBatchNumbersBaseLine = stLine.BatchNumbers.IndexOf(batch)
									};
									stLine.StockTransferLinesBinAllocations.Add(binAlloc);
								}

								//// NOT POSSIBLE BOTH LOCATION ARE BIN ENABLED : Need for enhancement
								bool binToEnable = false;
								binToEnable = (from t0 in db.OWHS where t0.BinActivat == "Y" && t0.WhsCode == line.WarehouseCode select t0.WhsCode).Any();
								if (binToEnable)
								{
									var binAlloc = new StockTransferLinesBinAllocation
									{
										BinAbsEntry = binEntry,
										Quantity = 1,
										BinActionType = BinActionTypeEnum.BatToWarehouse,
										SerialAndBatchNumbersBaseLine = stLine.BatchNumbers.IndexOf(batch)
									};
									stLine.StockTransferLinesBinAllocations.Add(binAlloc);
								}
								#endregion
							}
							stLine.Quantity = stLine.BatchNumbers.Sum(x => x.Quantity);
						}
						#endregion

						payload.StockTransferLines.Add(stLine);
						//pallet.Boxes


						var requests = new List<SLBatchRequest>();
						SLBatchRequest req1 = new(HttpMethod.Post, "StockTransfers", Serialize(payload), contentID: 1);
						requests.Add(req1);

						Dictionary<int, (int DocEntry, string U_ServiceType)> serviceMap = new();
						#region Create completed inventory transfers batch's status update requests
						var toBePostedBatches = new List<BatchNumberDetail>();
						foreach (var batch in payload.StockTransferLines.SelectMany(x => x.BatchNumbers))
						{
							var baseBatch = (from t0 in db.OBTN where t0.DistNumber == batch.BatchNumberProperty select t0).Single();
							var docnum = baseBatch.U_SONo.Value;

							(int DocEntry, string U_ServiceType) serviceSource;
							if (!serviceMap.TryGetValue(docnum, out serviceSource))
							{
								var orderData = (from t0 in db.ORDR
												 where t0.DocNum == docnum
												 select new { t0.DocEntry, t0.U_ServiceType }).Single();

								var tuple = (orderData.DocEntry, orderData.U_ServiceType);
								serviceMap.Add(docnum, tuple);
								serviceSource = tuple;
							}

							//var newStatus = Status.AtIrradiation;

							var batchPayload = new BatchNumberDetail
							{
								U_BatchStatus = data.UTransferType.ToLower().Contains("non-conformity") ? data.UTransferType : data.DisplayStatus,
								U_TIMS_ITSortCode = data.SortCodeStatus,//V2 pallet.DisplayStatus //V1 newStatus.GetDescription(),
								ItemCode = ItemCode
							};

							if (data.UTransferType.ToLower().Contains("for storage - receiving"))
							{
								batchPayload.U_TIMS_ITRecBin = DateTime.Now;
								batchPayload.U_TIMS_ITRecBinTime = TimeOfDay.Now;
							}
							else if (data.UTransferType.ToLower().Contains("for storage - unloading"))
							{
								batchPayload.U_TIMS_IUBBin = DateTime.Now;
								batchPayload.U_TIMS_IUBBinTime = TimeOfDay.Now;
								batchPayload.U_TIMS_OngoingStatus = "Complete";
							}
							else if (data.UTransferType.ToLower().Contains("for irradiation"))
							{
								batchPayload.U_TIMS_ITBinILB = DateTime.Now;
								batchPayload.U_TIMS_ITBinILBTime = TimeOfDay.Now;
								batchPayload.U_TIMS_OngoingStatus = "At Irradiation";
							}
							else if (data.UTransferType.ToLower().Equals("at irradiation loading"))
							{
								batchPayload.U_TIMS_IrradLoading = DateTime.Now;
								batchPayload.U_TIMS_IrradLoadTime = TimeOfDay.Now;
								batchPayload.U_TIMS_OngoingStatus = "For Unloading";
							}
							else if (data.UTransferType.ToLower().Contains("at irradiation")) // && line.WarehouseCode == "U")
							{
								batchPayload.U_TIMS_IrradUnloading = DateTime.Now;
								batchPayload.U_TIMS_IUBTime = TimeOfDay.Now;
							}
							else if (data.UTransferType.ToLower().Contains("For Dispatch"))
							{
								batchPayload.U_TIMS_ITBinDispDate = DateTime.Now;
								batchPayload.U_TIMS_ITBinDispTime = TimeOfDay.Now;
								//batchPayload.U_TIMS_IT //U_TIMS_ITBinDispDate 
							}


							SLBatchRequest batchStatusUpdate = new(HttpMethod.Patch, $"BatchNumberDetails({baseBatch.AbsEntry})", Serialize(batchPayload), contentID: requests.Count + 1);
							requests.Add(batchStatusUpdate);

							batchPayload.Batch = baseBatch.DistNumber;
							toBePostedBatches.Add(batchPayload);
							////Update Sales Order UDF Status
							var Order = new Document
							{
								U_SOStatus = data.DisplayStatus,
							};
						}

						#region Sales Order Status Update (Per pallet)

						//if(line.BatchNumbers.Count == 0)
						//{
						//	var batchesDict = toBePostedBatches.ToDictionary(x => x.Batch);

						//	// count of batches with status update that is the same as the current transaction status
						//	var currentCount = pallet.Boxes.Where(x => !batchesDict.ContainsKey(x.Id))
						//		.Count(x => data.DisplayStatus.Contains(x!.Status, StringComparison.InvariantCultureIgnoreCase));

						//	var toBePostedCount = toBePostedBatches.Count(x => x.U_BatchStatus.Contains(data.DisplayStatus, StringComparison.InvariantCultureIgnoreCase));

						//	int total = currentCount + toBePostedCount;
						//	int notNonConformityTotal = pallet.Boxes.Count(x => !x.Status.Contains("non-conformity", StringComparison.InvariantCultureIgnoreCase));

						//	if (notNonConformityTotal == total)
						//	{
						//		var salesOrderDocEntry = (from t0 in db.ORDR where t0.DocNum == pallet.SalesOrderDocNum select t0.DocEntry).First();
						//		var order = new Document
						//		{
						//			U_SOStatus = data.DisplayStatus
						//                          };
						//		SLBatchRequest OrderStatusUpdate = new(HttpMethod.Patch, $"Orders({salesOrderDocEntry})", Serialize(order), contentID: requests.Count + 1);
						//		requests.Add(OrderStatusUpdate);

						//	}
						//}

						#endregion

						#endregion
						var resps = await _sl.BatchAsync(requests.ToArray());
						var errors = resps.Where(x => !x.IsSuccessStatusCode);
						if (errors.Any())
						{
							var resp = errors.First();
							var error = $"Error in Transfer: {await resp.Content.ReadAsStringAsync()}";
							throw new Exception(error);
							resp.EnsureSuccessStatusCode();
						}
					}

					//RESET DB TRACKING AFTER POSTING TO SAP
					db.ChangeTracker.Clear();

					#region Sales Order Status Updating
					var pallets = data.InventoryTransferLines.Select(x => x.PalletCode).Distinct();
					var soUpdateRequests = new List<SLBatchRequest>();

					//Kunin yung pinakamaraming status sa Batch Number Transaction Report where status <> non confomity -Karl 12/15/2023
					var soItemCode = data.InventoryTransferLines.Select(x => x.ItemCode).Distinct().FirstOrDefault();
					var soDocNum = pallets.FirstOrDefault().Split("-")[0];

					var totalItemCount = db.OBTN.Count(x => x.ItemCode == soItemCode && x.U_SONo == Convert.ToInt32(soDocNum));

					var soStatus = db.OBTN.Where(x => x.ItemCode == soItemCode && x.U_SONo == Convert.ToInt32(soDocNum) && (x.U_TIMS_ITSortCode ?? 1) != -1)
						.GroupBy(table => table.U_BatchStatus)
						.Select(group => new
						{
							Status = group.Key,
							Count = group.Count()
						})
						.OrderByDescending(x => x.Count)
						.FirstOrDefault();

					var soNewStatus = db.OBTN.Where(x => x.ItemCode == soItemCode && x.U_SONo == Convert.ToInt32(soDocNum) && x.U_BatchStatus == soStatus.Status).FirstOrDefault();

					var lastServiceDataOrder = (from t0 in db.SERVICE_DATA_ROW
												where t0.U_TransferType == data.UTransferType
												select t0).OrderByDescending(x => x.LineId).FirstOrDefault();

					if (soNewStatus.U_TIMS_ITSortCode == (lastServiceDataOrder?.U_SortCode ?? -1) && soStatus.Count != totalItemCount)
					{
						// Find the status with the next highest count
						var nextHighestStatus = db.OBTN
							.Where(x => x.ItemCode == soItemCode && x.U_SONo == Convert.ToInt32(soDocNum) && (x.U_TIMS_ITSortCode ?? 1) != -1 && (x.U_TIMS_ITSortCode ?? 1) != lastServiceDataOrder.U_SortCode)
							.GroupBy(table => table.U_BatchStatus)
							.Select(group => new
							{
								Status = group.Key,
								Count = group.Count()
							})
							.OrderByDescending(x => x.Count)
							.FirstOrDefault();

						if (nextHighestStatus != null)
						{
							soStatus = nextHighestStatus;
						}
					}

					var order = new Document
					{
						DocEntry = Convert.ToInt32(soDocNum),
					};

					#region For Receiving Trigger in Dashboard Notification
					//GET FIRST SORT ORDER
					string qryFirstSortOrder = $@"SELECT T0.U_TransferType, T0.U_DisplayStatus 
							FROM [@SERVICE_DATA_ROW] T0 
							INNER JOIN ORDR T1 
							ON T0.Code = T1.U_ServiceType 
							WHERE T0.U_SortCode = 1
							AND T1.DocNum = {soDocNum}";
					var constr2 = _conf.GetConnectionString("SAP");
					var firstSortOrderqry = _mysql.GetData(qryFirstSortOrder, constr2, CommandType.Text);
					var firstSortOrder = firstSortOrderqry.Select(t0 => new { t0.U_TransferType, t0.U_DisplayStatus }).FirstOrDefault();

					if (!(data.UTransferType.ToLower().Contains(firstSortOrder.U_TransferType.ToString().ToLower())))
					{
						if (!data.UTransferType.ToLower().Contains("for dispatch"))
						{
							order.U_SOStatus = soStatus.Status;
						}
					}
					else
					{
						//CHANGE SO STATUS TO RECEIVED IF ALL PALLETS ARE AT 1ST SORT ORDER (RECEIVING) ONLY
						var palletCount = (from t0 in db.OBTN
										   where t0.U_SONo == Convert.ToInt32(soDocNum)
										   select t0).ToList();
						if (palletCount.Count == palletCount.Count(x => x.U_TIMS_ITSortCode == 1))
						{
							order.U_SOStatus = firstSortOrder.U_DisplayStatus;
						}
					}
					#endregion


					if (data.UTransferType.ToLower().Contains("for irradiation"))
					{
						order.U_EBStatus = "At Irradiation";
					}
					else if (data.UTransferType.ToLower().Contains("for storage - unloading"))
					{
						string qrySortOrderBeforeDispatch = $@"SELECT TOP 1 T0.U_SortCode 
							FROM [@SERVICE_DATA_ROW] T0 
							INNER JOIN ORDR T1 
							ON T0.Code = T1.U_ServiceType 
							WHERE T0.U_SortCode <> (SELECT TOP 1 X.U_SortCode FROM [@SERVICE_DATA_ROW] X WHERE X.Code = T1.U_ServiceType ORDER BY U_SortCode DESC)
							AND T1.DocNum = {soDocNum}
							ORDER BY U_SortCode DESC";
						var sortOrderBeforeDispatchqry = _mysql.GetData(qrySortOrderBeforeDispatch, constr2, CommandType.Text);
						var sortOrderBeforeDispatch = sortOrderBeforeDispatchqry.Select(t0 => new { t0.U_SortCode }).FirstOrDefault();

						//CHANGE EB STATUS TO FOR DISPATCH IF ALL PALLETS ARE AT BEFORE DISPATCH
						var palletCount = (from t0 in db.OBTN
										   where t0.U_SONo == Convert.ToInt32(soDocNum)
										   select t0).ToList();
						if (palletCount.Count == palletCount.Where(x => x.U_TIMS_ITSortCode == Convert.ToInt32(sortOrderBeforeDispatch.U_SortCode)).Count())
						{
							order.U_EBStatus = "For Dispatch";
						}
					}

					SLBatchRequest OrderStatusUpdate = new(HttpMethod.Patch, $"Orders({soDocNum})", Serialize(order), contentID: soUpdateRequests.Count + 1);
					soUpdateRequests.Add(OrderStatusUpdate);

					//foreach (var pallet in pallets)
					//{
					//	Pallet refPallet = _palletService.Get(pallet);

					//	var noNonConformityBatches = refPallet.Boxes.Where(x => !x.Id.Contains("non-conformity"));
					//	var uniqueStatus = noNonConformityBatches.Select(x => x.Status).Distinct();
					//	var allIsSame = uniqueStatus.Count() == 1;

					//	if (!allIsSame) break;

					//	var soStatus = uniqueStatus.Single();
					//	var salesOrderDocEntry = (
					//		from t0 in db.ORDR where t0.DocNum == refPallet.SalesOrderDocNum select t0.DocEntry).First();
					//	var order = new Document
					//	{
					//		U_SOStatus = soStatus
					//	};
					//	SLBatchRequest OrderStatusUpdate = new(HttpMethod.Patch, $"Orders({salesOrderDocEntry})", Serialize(order), contentID: soUpdateRequests.Count + 1);
					//	soUpdateRequests.Add(OrderStatusUpdate);

					//}
					var soResps = await _sl.BatchAsync(soUpdateRequests.ToArray());
					var soReqErrors = soResps.Where(x => !x.IsSuccessStatusCode);
					if (soReqErrors.Any())
					{
						var resp = soReqErrors.First();
						var error = $"Error in Sales Order: {await resp.Content.ReadAsStringAsync()}";
						throw new Exception(error);
						resp.EnsureSuccessStatusCode();
					}
					#endregion

					#region Unassign bin if sort order = 3 (temporary hardcoded) -KARL 2023/12/13
					if ((serviceData?.U_SortCode ?? 0) == 3)
					{
						//REMOVE CLEAR OUT BIN AFTER TRANSFERING - KARL 12/13/2023
						if (!_binDataService.ClearPalletLabel(data.InventoryTransferLines.Select(x => x.PalletCode).ToList()))
						{
							throw new Exception($"Clearing Pallets Failed.");
						}
					}
					#endregion

					#region Assign bin if sort order = 5 (temporary hardcoded) -KARL 2023/12/13
					if ((serviceData?.U_SortCode ?? 0) == 5)
					{
						List<BinAssignment> binAss = new List<BinAssignment>();
						foreach (var bin in data.InventoryTransferLines)
						{
							string WarehouseCode = bin.BinCode.Split("-")[0];
							binAss.Add(
								new BinAssignment
								{
									BinCode = bin.BinCode,
									PalletNo = bin.PalletCode,
									SONo = data.DocNum.ToString(),
									WarehouseCode = WarehouseCode
								}
							);
						}

						_binDataService.SavePalletLabel(binAss);
					}
					#endregion
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private async Task PostAtIrradiation(InventoryTransferModel data)
		{
			if (data.DocDate.Year < 2000) data.DocDate = DateTime.Now;
			var payload = new StockTransfer
			{
				DocDate = data.DocDate,
				U_Remarks = _utilities.GetPostRemarks()
			};

			string sourceWarehouse = data.LocationCode;
			List<Pallet> toBePostedPallets = new List<Pallet>();
			foreach (var pallet in from line in data.InventoryTransferLines
								   let palletCode = line.PalletCode
								   let pallet = _palletService.Get(palletCode)
								   select pallet//pallet.Boxes
			)
			{
				toBePostedPallets.Add(pallet);
				var stLine = new StockTransferLine()
				{
					ItemCode = pallet.ItemCode,
					FromWarehouseCode = "L",
					WarehouseCode = "U"
				};
				foreach (var box in pallet.Boxes)
				{
					if (box.Status is null) continue;
					var batch = new BatchNumber
					{
						BatchNumberProperty = box.Id,
						Quantity = 1,
						U_BatchStatus = Status.Irradiated_ForQA.GetDescription()
					};

					if (sourceWarehouse == "L" && data.TransferType == TransferTypeEnum.AtIrradiation)
					{
						// update date and time if from loading to unloading irradiation
						batch.U_TIMS_IrradUnloading = DateTime.Now;
						batch.U_TIMS_IUBTime = TimeOfDay.Now;
					}

					stLine.BatchNumbers.Add(batch);
				}

				stLine.Quantity = stLine.BatchNumbers.Sum(x => x.Quantity);
				payload.StockTransferLines.Add(stLine);
			}

			var docnums = toBePostedPallets.Select(x => x.SalesOrderDocNum).Distinct();
			var toBePostedPalletCodes = toBePostedPallets.Select(x => x.Code).Distinct();
			List<int> completedTransfers = new();

			foreach (var docnum in docnums)
			{
				var soPallet = _salesOrderService.GetPallets(docnum).Where(x => !toBePostedPalletCodes.Contains(x.Code)).ToList();
				bool noPalletsToBeReceived;
				using (var db = _sapDbFactory.CreateDbContext())
				{
					var receivedPallets = db.OBTN.Where(x => !string.IsNullOrEmpty(x.MnfSerial) && x.U_SONo == docnum).Select(x => x.MnfSerial).ToList();
					noPalletsToBeReceived = soPallet.TrueForAll(x => receivedPallets.Contains(x.Code));
				}
				if (noPalletsToBeReceived)
					completedTransfers.Add(docnum);
			}

			try
			{
				var requests = new List<SLBatchRequest>();
				SLBatchRequest req1 = new(HttpMethod.Post, "StockTransfers", Serialize(payload), contentID: 1);
				requests.Add(req1);
				using (var db = _sapDbFactory.CreateDbContext())
				{
					Dictionary<int, (int DocEntry, string U_ServiceType)> serviceMap = new();
					#region Create completed inventory transfers batch's status update requests
					foreach (var batch in payload.StockTransferLines.SelectMany(x => x.BatchNumbers))
					{
						var baseBatch = (from t0 in db.OBTN where t0.DistNumber == batch.BatchNumberProperty select t0).Single();
						var docnum = baseBatch.U_SONo.Value;

						(int DocEntry, string U_ServiceType) serviceSource;
						if (!serviceMap.TryGetValue(docnum, out serviceSource))
						{
							var orderData = (from t0 in db.ORDR
											 where t0.DocNum == docnum
											 select new { t0.DocEntry, t0.U_ServiceType }).Single();

							var tuple = (orderData.DocEntry, orderData.U_ServiceType);
							serviceMap.Add(docnum, tuple);
							serviceSource = tuple;
						}

						var newStatus = Status.Irradiated_ForQA;

						var batchPayload = new BatchNumberDetail
						{
							U_BatchStatus = newStatus.GetDescription(),
							U_TIMS_IrradUnloading = DateTime.Now,
							U_TIMS_IUBTime = TimeOfDay.Now
						};
						SLBatchRequest batchStatusUpdate = new(HttpMethod.Patch, $"BatchNumberDetails({baseBatch.AbsEntry})", Serialize(batchPayload), contentID: requests.Count + 1);
						requests.Add(batchStatusUpdate);
					}
					#endregion
				}
				var resps = await _sl.BatchAsync(requests.ToArray());
				var errors = resps.Where(x => !x.IsSuccessStatusCode);
				if (errors.Any())
				{
					var resp = errors.First();
					var error = await resp.Content.ReadAsStringAsync();
					resp.EnsureSuccessStatusCode();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		private async Task PostForRelease(InventoryTransferModel data)
		{
			// TODO: Implement SAP service layer posting of inventory transfer logic for Release transfer type
		}

		private async Task PostForIrradiation(InventoryTransferModel data)
		{
			if (data.DocDate.Year < 2000) data.DocDate = DateTime.Now;
			var payload = new StockTransfer
			{
				DocDate = data.DocDate,
				U_Remarks = _utilities.GetPostRemarks()
			};

			List<Pallet> toBePostedPallets = new List<Pallet>();

			Dictionary<int, ServiceType> serviceTypeMap = new();

			foreach (var line in data.InventoryTransferLines)
			{
				var palletCode = line.PalletCode;
				Pallet pallet = _palletService.Get(palletCode);
				toBePostedPallets.Add(pallet);

				ServiceType serviceType;

				if (!serviceTypeMap.TryGetValue(pallet.SalesOrderDocNum, out serviceType))
				{
					var docnum = pallet.SalesOrderDocNum;
					var salesOrderDocEntry = _salesOrderService.GetDocEntry(pallet.SalesOrderDocNum);
					serviceType = _salesOrderService.GetServiceType(salesOrderDocEntry);
					serviceTypeMap.Add(docnum, serviceType);
				}

				//string fromWarehouse = serviceType == ServiceType.IrradiationOnly ? "RC" : pallet.WarehouseCode;
				string fromWarehouse = serviceType == ServiceType.IrradiationOnly ? "RC" : data.LocationCode;
				var stLine = new StockTransferLine()
				{
					ItemCode = pallet.ItemCode,
					FromWarehouseCode = fromWarehouse,
					WarehouseCode = "L"
				};

				foreach (var box in pallet.Boxes)
				{
					if (box.Status is null) continue;
					var batch = new BatchNumber
					{
						BatchNumberProperty = box.Id,
						Quantity = 1,
					};
					stLine.BatchNumbers.Add(batch);

					if (serviceType == ServiceType.StorageIrradiateStorage)
					{
						var binAlloc = new StockTransferLinesBinAllocation
						{
							BinAbsEntry = pallet.BinEntry,
							Quantity = 1,
							BinActionType = BinActionTypeEnum.BatFromWarehouse,
							SerialAndBatchNumbersBaseLine = stLine.BatchNumbers.IndexOf(batch)
						};

						stLine.StockTransferLinesBinAllocations.Add(binAlloc);
					}
				}
				stLine.Quantity = stLine.BatchNumbers.Sum(x => x.Quantity);

				payload.StockTransferLines.Add(stLine);
				//pallet.Boxes

				try
				{
					var requests = new List<SLBatchRequest>();
					SLBatchRequest req1 = new(HttpMethod.Post, "StockTransfers", Serialize(payload), contentID: 1);
					requests.Add(req1);
					using (var db = _sapDbFactory.CreateDbContext())
					{

						Dictionary<int, (int DocEntry, string U_ServiceType)> serviceMap = new();
						#region Create completed inventory transfers batch's status update requests
						foreach (var batch in payload.StockTransferLines.SelectMany(x => x.BatchNumbers))
						{
							var baseBatch = (from t0 in db.OBTN where t0.DistNumber == batch.BatchNumberProperty select t0).Single();
							var docnum = baseBatch.U_SONo.Value;

							(int DocEntry, string U_ServiceType) serviceSource;
							if (!serviceMap.TryGetValue(docnum, out serviceSource))
							{
								var orderData = (from t0 in db.ORDR
												 where t0.DocNum == docnum
												 select new { t0.DocEntry, t0.U_ServiceType }).Single();

								var tuple = (orderData.DocEntry, orderData.U_ServiceType);
								serviceMap.Add(docnum, tuple);
								serviceSource = tuple;
							}

							var newStatus = Status.AtIrradiation;

							var batchPayload = new BatchNumberDetail
							{
								U_BatchStatus = newStatus.GetDescription(),
								U_TIMS_IrradLoading = DateTime.Now,
								U_TIMS_IrradLoadTime = TimeOfDay.Now

							};
							SLBatchRequest batchStatusUpdate = new(HttpMethod.Patch, $"BatchNumberDetails({baseBatch.AbsEntry})", Serialize(batchPayload), contentID: requests.Count + 1);
							requests.Add(batchStatusUpdate);
						}
						#endregion
					}
					var resps = await _sl.BatchAsync(requests.ToArray());
					var errors = resps.Where(x => !x.IsSuccessStatusCode);
					if (errors.Any())
					{
						var resp = errors.First();
						var error = await resp.Content.ReadAsStringAsync();
						throw new Exception(error);
						resp.EnsureSuccessStatusCode();
					}
				}
				catch (Exception ex)
				{
					throw;
				}

			}
		}

		public InventoryTransferModel Get(int id)
		{
			throw new NotImplementedException();
		}

		public List<InventoryTransferModel> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<List<InventoryTransferModel>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<InventoryTransferModel> GetAsync(int id)
		{
			//try
			//{
			//    var resps = await _sl.GetAsync<InventoryTransferViewModel>("StockTransfers", id);
			//    return resps;
			//}
			//catch (Exception ex)
			//{
			//    return new InventoryTransferViewModel();
			//    throw;
			//}
			throw new NotImplementedException();
		}
		public Pallet GetPalletMatrix(string itemCode, string palletCode, string binCode, string serviceType, [Optional] bool isManualTransfer)
		{
			using (var db = _sapDbFactory.CreateDbContext())
			{
				Pallet pallet = new();
				pallet.ItemCode = itemCode;
				pallet.Code = palletCode;
				pallet.BinCode = binCode;
				pallet.ServiceType = serviceType;
				#region GET_BATCH_STATUS
				//var batchdata = (from t0 in db.OBTN
				//                 where t0.MnfSerial == id && t0.ItemCode == pallet.ItemCode
				//                 select new { t0 }).FirstOrDefault();
				//string sqry2 = $@"SELECT top 1 U_BatchStatus, isnull(U_TIMS_ITSortCode,-1) as U_TIMS_ITSortCode FROM [OBTN] Where MnfSerial= '{pallet.Code}' AND ItemCode = '{pallet.ItemCode}' ";

				//GET THE LATEST SORT ORDER EXCEPT THE LAST SORT ORDER - KARL 11/27/2023
				//MUST INCLUDE THE LAST SORT ORDER - KARL 12/12/2023
				//string sqry2 = $@"SELECT top 1 
				//					U_BatchStatus
				//					, isnull(U_TIMS_ITSortCode,-1) as U_TIMS_ITSortCode 
				//				FROM 
				//					[OBTN] 
				//				Where 
				//					MnfSerial= '{pallet.Code}' 
				//				AND 
				//					ItemCode = '{pallet.ItemCode}' 
				//				--AND 
				//					--isnull(U_TIMS_ITSortCode,-1) <> (SELECT TOP 1 U_SortCode FROM [@SERVICE_DATA_ROW] WHERE Code = '{pallet.ServiceType}' ORDER BY U_SortCode DESC)
				//				ORDER BY
				//					U_TIMS_ITSortCode 
				//				DESC;";

				string sqry2 = $@"
						WITH YourFirstQuery AS (
							SELECT distinct 
								T0.LocCode
								,(SELECT DISTINCT TOP 1  X.LogEntry 
									FROM OITL X
									inner join ITL1 Y on Y.ItemCode = X.ItemCode and Y.LogEntry = X.LogEntry
									inner join OBTN Z on Z.ItemCode = Y.ItemCode and Z.SysNumber = Y.SysNumber
									WHERE 
									T0.ItemCode = '{itemCode}'
									and Z.MnfSerial = '{palletCode}'
									and Z.U_SONo = {palletCode.Split("-")[0]}
									and X.ApplyType = 67
									and X.DocQty > 0
									and X.LocCode = T0.LocCode
									ORDER BY X.LogEntry DESC
									) ""LogEntry""
							FROM 
							  OITL T0 
							  inner join ITL1 T1 on T1.ItemCode = T0.ItemCode and T1.LogEntry = T0.LogEntry
							  inner join OBTN T2 on T2.ItemCode = T1.ItemCode and T2.SysNumber = T1.SysNumber
							WHERE 
								T0.ItemCode = '{itemCode}'
								and T2.MnfSerial = '{palletCode}'
								and T2.U_SONo = {palletCode.Split("-")[0]}
								and T0.ApplyType = 67
								and T0.DocQty > 0
								and T0.LocCode <> 'NC' 
						),
						OrderedResults AS (
							SELECT LogEntry, LocCode,
								   ROW_NUMBER() OVER (ORDER BY LogEntry desc) AS RowPriority
							FROM YourFirstQuery
						),
						CalculatedResults AS (
							SELECT DISTINCT TOP 1
								Case 
									when (select top 1 ISNULL(X.U_WarehouseCode, '') from ""@SERVICE_DATA_ROW"" X WHERE X.U_WarehouseCode = A.LocCode AND X.Code = '{serviceType}' AND A.RowPriority = 1) <> ''
										THEN
											(select top 1 X.U_SortCode from ""@SERVICE_DATA_ROW"" X WHERE X.U_WarehouseCode = A.LocCode AND X.Code = '{serviceType}')
										ELSE
											ISNULL((select top 1 X.U_SortCode from ""@SERVICE_DATA_ROW"" X WHERE X.U_WarehouseCode = A.LocCode AND X.Code = '{serviceType}' AND A.RowPriority = 2),0) + 1
								END AS ""U_SortCode""
							FROM OrderedResults A
							ORDER BY ""U_SortCode"" desc
						)
						SELECT 
							A.U_SortCode ""U_TIMS_ITSortCode""
							,(select top 1 X.U_DisplayStatus from ""@SERVICE_DATA_ROW"" X WHERE X.Code = '{serviceType}' AND X.U_SortCode = A.U_SortCode) ""U_BatchStatus""
						FROM CalculatedResults A

						UNION ALL

						SELECT -1 ""U_TIMS_ITSortCode"", (select top 1 X.U_DisplayStatus from ""@SERVICE_DATA_ROW"" X WHERE X.Code = '{serviceType}' ORDER BY X.U_SortCode) ""U_BatchStatus"" -- Replace with actual dummy values
						WHERE NOT EXISTS (SELECT TOP 1 1 FROM CalculatedResults);;
				";

				var constr2 = _conf.GetConnectionString("SAP");
				var data2 = _mysql.GetData(sqry2, constr2, CommandType.Text);
				var batchdata = data2.Select(t0 => new { t0.U_BatchStatus, t0.U_TIMS_ITSortCode }).FirstOrDefault();

				#endregion

				var bin = db.OBIN.Where(x => x.BinCode == pallet.BinCode).Select(x => new { x.WhsCode }).SingleOrDefault();
				if (bin is null && (batchdata.U_TIMS_ITSortCode != 3 && batchdata.U_TIMS_ITSortCode != 4 && batchdata.U_TIMS_ITSortCode != 5))
				{
					throw new Exception($"[SAP] BinCode: {pallet.BinCode} is not defined");
				}

				if (batchdata != null)
				{
					#region GET_MATRIX_FROM_UDO
					string _code = pallet.ServiceType;
					string _bstat = batchdata.U_BatchStatus;
					//var serviceData = (from t0 in db.SERVICE_DATA_ROW
					//                   where t0.Code == _code && t0.U_DisplayStatus == _bstat
					//                   select new { t0 }).FirstOrDefault();
					string sqry = $@"SELECT Code, LineId , U_SortCode FROM [@SERVICE_DATA_ROW] Where Code= '{pallet.ServiceType}' AND U_SortCode = '{batchdata.U_TIMS_ITSortCode}' ";
					var constr = _conf.GetConnectionString("SAP");
					var data = _mysql.GetData(sqry, constr, CommandType.Text);
					var _serviceData = data.Select(t0 => new { t0.U_SortCode }).FirstOrDefault();

					if (_serviceData != null)
					{
						if (isManualTransfer)
						{
							//CHECK IF THE PALLET HAS NON-COMFORMITY - KARL 11/27/2023
							var nonConformity = db.OBTN.Where(x => x.MnfSerial == palletCode && x.ItemCode == itemCode && x.U_TIMS_ITSortCode == -1).Select(x => new { x.U_TIMS_ITSortCode }).ToList();
							if (nonConformity.Count > 0)
							{
								//RETAIN CURRENT LAST SORT ORDER - KARL 11/27/2023
								sqry = $@"SELECT top 1 * FROM [@SERVICE_DATA_ROW] WHERE Code = '{pallet.ServiceType}' AND U_SortCode = '{_serviceData.U_SortCode}'";
								data = _mysql.GetData(sqry, constr, CommandType.Text);
								var nonConformServiceData = data.Select(t0 => new { t0 }).FirstOrDefault();
								if (nonConformServiceData == null) throw new Exception($"You have already processed the last step!");
								pallet.TransferType = nonConformServiceData.t0.U_TransferType;
								pallet.DisplayStatus = nonConformServiceData.t0.U_DisplayStatus;
								pallet.SortCodeStatus = nonConformServiceData.t0.U_SortCode;
								pallet.WarehouseCode = nonConformServiceData.t0.U_WarehouseCode == "SAO_" ? bin.WhsCode : nonConformServiceData.t0.U_WarehouseCode;
								pallet.FromWarehouseCode = nonConformServiceData.t0.U_FromWarehouseCode == "SAO_" ? bin.WhsCode : nonConformServiceData.t0.U_FromWarehouseCode;

								//RETURN
								return pallet;
							}
						}

						////Proceed to the next step
						sqry = $@"SELECT top 1 * FROM [@SERVICE_DATA_ROW] WHERE Code= '{pallet.ServiceType}' AND U_SortCode > '{_serviceData.U_SortCode}' ORDER BY U_SortCode ASC ";
						data = _mysql.GetData(sqry, constr, CommandType.Text);
						var serviceData = data.Select(t0 => new { t0 }).FirstOrDefault();
						if (serviceData == null) throw new Exception($"You have already processed the last step!");
						pallet.TransferType = serviceData.t0.U_TransferType;
						pallet.DisplayStatus = serviceData.t0.U_DisplayStatus;
						pallet.SortCodeStatus = serviceData.t0.U_SortCode;
						pallet.WarehouseCode = serviceData.t0.U_WarehouseCode == "SAO_" ? (bin?.WhsCode ?? "") : serviceData.t0.U_WarehouseCode;
						pallet.FromWarehouseCode = serviceData.t0.U_FromWarehouseCode == "SAO_" ? (bin?.WhsCode ?? "") : serviceData.t0.U_FromWarehouseCode;
					}
					else
					{
						//serviceData = (from t0 in db.SERVICE_DATA_ROW
						//               where t0.Code == pallet.ServiceType && t0.U_SortCode == 1
						//               select new { t0 }).FirstOrDefault();
						////Proceed to the next step
						sqry = $@"SELECT top 1 * FROM [@SERVICE_DATA_ROW] WHERE Code= '{pallet.ServiceType}' ORDER BY U_SortCode ASC ";
						data = _mysql.GetData(sqry, constr, CommandType.Text);
						var serviceData = data.Select(t0 => new { t0 }).FirstOrDefault();
						if (serviceData == null) throw new Exception($"[SAP] Service Type: {batchdata.U_BatchStatus} is not configured");
						pallet.TransferType = serviceData.t0.U_TransferType;
						pallet.DisplayStatus = serviceData.t0.U_DisplayStatus;
						pallet.SortCodeStatus = serviceData.t0.U_SortCode;
						pallet.WarehouseCode = serviceData.t0.U_WarehouseCode == "SAO_" ? (bin?.WhsCode ?? "") : serviceData.t0.U_WarehouseCode;
						pallet.FromWarehouseCode = serviceData.t0.U_FromWarehouseCode == "SAO_" ? (bin?.WhsCode ?? "") : serviceData.t0.U_FromWarehouseCode;
					}
					#endregion
				}
				else
				{
					throw new Exception($"[SAP] Pallet: {pallet.BinCode} in Batch is not defined");
				}
				return pallet;
			}
		}

		public List<BatchSerialViewModel.BatchSerial> GetPalletBatches(string palletCode)
		{
			var (soStr, palletSeries, BoxNo) = _receivingService.GetPalletDetails(palletCode);
			var so = int.Parse(soStr);
			using (var db = _sapDbFactory.CreateDbContext())
			{
				var qryResult = from t0 in db.OBTN
								join t1 in db.OITM on t0.ItemCode equals t1.ItemCode
								where t0.MnfSerial == palletCode
								select new { t0, t1 };
				var data = qryResult.ToList();
				var mapped = data.Select(result => new BatchSerialViewModel.BatchSerial
				{
					DistNumber = result.t0.DistNumber,
					MnfSerial = result.t0.MnfSerial,
					Quantity = 1,
					ItemCode = result.t1.ItemCode,
				}).ToList();

				return mapped;
			}
		}

		public (bool isValid, string message) ValidateScannedPallet(Pallet pallet, string binCode, TransferTypeEnum transferType)
		{
			switch (transferType)
			{
				case TransferTypeEnum.ForStorage: return ValidateForStorage(pallet, binCode);
				case TransferTypeEnum.ForIrradiation: return ValidateForIrradiation(pallet, binCode);
				case TransferTypeEnum.AtIrradiation: return ValidateForIrradiation(pallet, binCode);
				default: throw new InvalidOperationException("Tranfer Type not supported");
			}
		}

		private (bool isValid, string message) ValidateForStorage(Pallet pallet, string binCode)
		{
			var isReceived = _salesOrderService.StatusIs(pallet.SalesOrderDocNum, "Received - For Storage");
			if (!isReceived)
			{
				return (false, "This Item is not for storage in Bin. Please coordinate with QA/QC Inspector");
			}

			if (pallet.WarehouseCode != "U") // TODO: check if bin is valid
				if (!pallet.BinCode.Equals(binCode))
				{
					return (false, $"Incorrect bin. Please proceed to Bin {pallet.BinCode}");
				}
				else
				{
					using (var db = _sapDbFactory.CreateDbContext())
					{
						var exists = db.OBIN.Where(x => x.BinCode == pallet.BinCode).Any();
						if (!exists) return (false, $"Bin {pallet.BinCode} doesn't exist");
					}
				}

			return (true, string.Empty);
		}

		private (bool isValid, string message) ValidateForIrradiation(Pallet pallet, string binCode)
		{

			return (true, string.Empty);
		}
		//public List<InventoryTransferBatch> GetPallet(string palletCode, string BinCode)
		//{
		//    var model = new List<InventoryTransferBatch>();
		//    //To do
		//    return model;
		//}
		public (string SoNum, string PalletSeries, double BoxNo) GetPalletDetails(string palletCode)
		{
			string so, pal;
			double box;

			if (string.IsNullOrEmpty(palletCode)) throw new ArgumentNullException("palletCode");

			string[] extractedData = palletCode.Split('-');
			if (extractedData.Length < 3) return (string.Empty, string.Empty, 0);

			so = extractedData[0];
			pal = extractedData[1];

			string strBoxNo = extractedData[2];
			double.TryParse(strBoxNo, out box);

			return (so, pal, box);
		}
		public async Task<InventoryTransferViewModel.ServiceData> GetServiceData(string serviceType)
		{
			try
			{
				var resps = await _sl.GetAsync<InventoryTransferViewModel.ServiceData>("SERVICE_DATA", serviceType);
				return resps;
			}
			catch (Exception ex)
			{
				return new InventoryTransferViewModel.ServiceData();
				throw;
			}
		}
		public async Task<Dictionary<string, string>> GetTransferType()
		{
			try
			{
				//var resps = await _sl.GetAsync<Dictionary<string,string>>("SERVICE_DATA");
				//return resps;\
				string query = $@"SELECT * FROM [@TRANSFER_TYPE]";
				var constr = _conf.GetConnectionString("SAP");
				var data = _mysql.GetData(query, constr, CommandType.Text);
				var res = data.ToDictionary(x => x.Code as string, x => x.Name as string);
				return res;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public Task<BinViewModel.BinMappingViewModel.BinAccumulator> GetAvailableBin(string itemCode, string whsCode, int absEntry)
		{
			try
			{
				string query = $@"SELECT * FROM [OBBQ]";
				var constr = _conf.GetConnectionString("SAP");
				var data = _mysql.GetData(query, constr, CommandType.Text).Select(x => new BinViewModel.BinMappingViewModel.BinAccumulator
				{
					AbsEntry = x["BinAbsEntry"],
					BinAbs = x["BinAbs"],
					SnBMDAbs = x["SnBMDAbs"],
					OnHandQty = x["OnHandQty"],
					WhsCode = x["WhsCode"],
				}).FirstOrDefault();
				return Task.FromResult(data);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<Dictionary<string, string>> GetTransferTypeFromMatrix(string transferType, string serviceType)
		{
			try
			{
				using (var db = _sapDbFactory.CreateDbContext())
				{
					//// Check the display status from the Matrix
					//// If TransferType not from the list then 
					////    Display Status: For Storage - Receiving && SortCode: 0
					//// Else if TransferType is Non-Conformity
					////    Display Status: Non-Conformity && SortCode: -1
					//// else
					////    Display Status & SortCode: From Matrix

					var serviceData = (from t0 in db.SERVICE_DATA_ROW
									   where t0.U_TransferType == transferType && t0.Code == serviceType
									   select t0
									   ).FirstOrDefault();

					var res = new Dictionary<string, string>();
					if (serviceData != null)
					{
						res.Add("DisplayStatus", serviceData.U_DisplayStatus);
						res.Add("SortCode", serviceData.U_SortCode.ToString());
					}
					else if (transferType.ToLower().Contains("non-conformity"))
					{
						res.Add("DisplayStatus", transferType);
						res.Add("SortCode", "-1");
					}
					return res;
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public bool BinOccupied(string BinCode, string PalletCode)
		{
			return _binDataService.BinOccupied(BinCode, PalletCode);
		}

		public bool BinExists(string BinCode)
		{
			try
			{
				using (var db = _sapDbFactory.CreateDbContext())
				{
					bool res = db.OBIN.Where(x => x.BinCode == BinCode).Any();
					return res;
				}
			}
			catch (Exception)
			{
				return false;
				//throw;
			}
		}
	}
}
