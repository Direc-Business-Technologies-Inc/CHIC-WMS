using Application.Libraries.SAP;
using Application.Libraries.SAP.DB.Models;
using Application.Libraries.SAP.ServiceLayer;
using Application.Libraries.SAP.SL;
using Application.Models.Models;
using B1SLayer;
using Dapper;
using DataManager.Libraries.Repositories;
using DataManager.Models.Enums;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using static Application.Models.ViewModels.DispatchViewModel;

namespace Application.Services.Core
{
	public class DispatchService : IDispatchService
	{
		private readonly IMySqlDataAccess _mysql;
		private readonly IConfiguration _conf;
		private readonly IServiceLayerDataAccess _sl;
		private readonly ISalesOrderService _salesOrderService;
		private readonly IUtilities _utilities;
		private readonly IPalletService _palletService;
		private readonly IServiceTypeService _serviceTypeService;
		private readonly IDbContextFactory<SapDb> _sapDbContextFactory;
		private readonly IMapper _mapper;
		private readonly IBinDataServices _binDataService;

		public DispatchService(IMySqlDataAccess mysql, IConfiguration conf, IServiceLayerDataAccess sl, IUtilities utilities, ISalesOrderService salesOrderService, IPalletService palletService, IServiceTypeService serviceTypeService, IDbContextFactory<SapDb> sapDbContextFactory, IMapper mapper, IBinDataServices binDataService)
		{
			_mysql = mysql;
			_conf = conf;
			_sl = sl;
			_utilities = utilities;
			_salesOrderService = salesOrderService;
			_palletService = palletService;
			_serviceTypeService = serviceTypeService;
			_sapDbContextFactory = sapDbContextFactory;
			_mapper = mapper;
			_binDataService = binDataService;
		}
		public DispatchModel Create(DispatchModel data)
		{
			throw new NotImplementedException();
		}
		private string Serialize(object data) => JsonConvert.SerializeObject(data, ServiceLayerExtensions.SerializerSettings);
		public async Task<DispatchModel> CreateAsync(DispatchModel data)
		{
			string[] pallets = data.Pallets.Where(x => x.ActualQuantity > 0).Select(x => x.Code).ToArray();
			if (!AllBoxIsReleased(pallets))
			{
				throw new Exception("Some pallets is not for release");
			}

			Document doc = new()
			{
				Comments = data.Remarks
			};

			DateTime currDateTime = DateTime.Now;
			string dispatchStatusStr = "Dispatched";

			var constr = _conf.GetConnectionString("SAP");
			string qry = "SELECT * FROM ORDR WHERE DocEntry = @entry";
			var res = _mysql.GetData<ORDR, object>(qry, new { entry = data.SalesOrderId }, constr, CommandType.Text).Single();

			doc.U_IrridiationDate = res.U_IrridiationDate;

			doc.U_IrridiationStart = _utilities.ToTimeOfDay(res.U_IrridiationStart.GetValueOrDefault());
			doc.U_IrridiationEnd = _utilities.ToTimeOfDay(res.U_IrridiationEnd.GetValueOrDefault());
			doc.U_PickUpDate = res.U_PickUpDate;
			doc.U_SONo = res.DocEntry.ToString();
			doc.U_ARDPNo = res.U_ARDPNo;
			doc.U_Remarks = _utilities.GetPostRemarks();
			doc.DocTotal = Convert.ToDouble(res.DocTotal);

			//string qryRdr1 = "SELECT * FROM RDR1 WHERE DocEntry = @entry and and isnull(U_ItemCode, '') <> ''";
			//var resRdr1 = _mysql.GetData<RDR1, object>(qry, new { entry = data.SalesOrderId }, constr, CommandType.Text).Single();

			string itemCode = res.U_CustItems;
			//string itemCode = resRdr1.U_ItemCode;

			string salesOrderServiceType = res.U_ServiceType;

			var serviceType = _serviceTypeService.Get(salesOrderServiceType);

			// TODO: Identify the dispatch warehouse source
			string fromWhse = serviceType.SERVICE_DATA_ROWCollection.OrderBy(x => x.U_SortCode).Last().U_WarehouseCode;
			/* 
            string fromWhse = serviceType
                .SERVICE_DATA_ROWCollection
                .OrderBy(x => x.U_SortCode)
                .Last().U_WarehouseCode;
            */


			//Get Sales order Document Lines
			string qryRdr1 = "SELECT * FROM RDR1 WHERE DocEntry = @entry";
			var resRdr1 = _mysql.GetData<RDR1, object>(qryRdr1, new { entry = data.SalesOrderId }, constr, CommandType.Text).ToList();

			//loop items based on SO (Copy From)
			foreach (var (rdr1, index) in resRdr1.Select((value, i) => (value, i)))
			{
				
				if (rdr1.ItemCode == itemCode)
				{
					//For Inventoriable Item
					var item = new DocumentLine
					{
						ItemCode = itemCode,
						WarehouseCode = fromWhse,
						BaseEntry = data.SalesOrderId,
						BaseLine = rdr1.LineNum,
						BaseType = Convert.ToInt32(rdr1.ObjType),
						//TaxCode = rdr1.TaxCode,
						//LineTotal = Convert.ToDouble(rdr1.LineTotal),
						//Price = Convert.ToDouble(rdr1.Price),
						//GrossPrice = Convert.ToDouble(rdr1.PriceAfVAT)
						//PriceAfterVAT = Convert.ToDouble(rdr1.PriceAfVAT)
					};

					doc.DocumentLines.Add(item);

					foreach (var pallet in data.Pallets)
					{
						var deets = _palletService.ExtractPalletDetails(pallet.Code);
						for (int i = 1; i <= pallet.ActualQuantity; i++)
						{
							BatchNumber batch = new()
							{
								BatchNumberProperty = string.Format("{0}-{1}", res.DocNum, (item.BatchNumbers.Count() + 1).ToString("D4")),
								Quantity = 1
							};
							item.BatchNumbers.Add(batch);
						}
					}
				}
				else
				{
					//For Services
					var item = new DocumentLine
					{
						ItemCode = rdr1.ItemCode,
						WarehouseCode = rdr1.WhsCode,
						BaseEntry = data.SalesOrderId,
						BaseLine = rdr1.LineNum,
						BaseType = Convert.ToInt32(rdr1.ObjType),
						Quantity = Convert.ToDouble(rdr1.Quantity),
						//LineTotal = Convert.ToDouble(rdr1.LineTotal),
						//Price = Convert.ToDouble(rdr1.Price),
						//GrossPrice = Convert.ToDouble(rdr1.PriceAfVAT)
						//PriceAfterVAT = Convert.ToDouble(rdr1.PriceAfVAT)
					};

					doc.DocumentLines.Add(item);
				}
			}


			// var reps = await _sl.PostAsync<Document, JObject>("InventoryGenEntries", jPayload);
			var payload = new
			{
				DocumentLines = doc.DocumentLines.Select(line => new
				{
					line.ItemCode,
					line.WarehouseCode,
					line.BaseEntry,
					line.BaseLine,
					line.BaseType,
					Quantity = line.ItemCode == itemCode ? line.BatchNumbers.Sum(b => b.Quantity) : line.Quantity,
					BatchNumbers = line.ItemCode == itemCode ? line.BatchNumbers.Select(batch => new
					{
						BatchNumber = batch.BatchNumberProperty,
						Quantity = 1
						//U_BatchStatus = Status.Received_ForStorage.GetDescription(),
					}) : Enumerable.Empty<object>(),
				}),
				doc.U_Remarks,
				doc.U_PickUpDate,
				doc.U_SONo,
				doc.U_ARDPNo,
				doc.Comments,
				//Add CardCode based on SO
				res.CardCode

				//U_SOStatus = Status.Received_ForStorage.GetDescription()
			};

			try
			{
				var strPayload = Serialize(payload);
				//var response = await _sl.PostAsync<JObject, object>("InventoryGenEntries", payload);

				//CHANGE FROM GI TO DR -KASO 2024/04/05
				//SLBatchRequest req1 = new(HttpMethod.Post, "InventoryGenExits", JsonConvert.SerializeObject(payload), contentID: 1);
				SLBatchRequest req1 = new(HttpMethod.Post, "DeliveryNotes", JsonConvert.SerializeObject(payload), contentID: 1);
				SLBatchRequest req2 = new(HttpMethod.Patch, $"Orders({res.DocEntry})", new
				{
					U_SOStatus = dispatchStatusStr
				}, contentID: 2);


				var batchRequests = new List<SLBatchRequest> { req1, req2 };

				using (var sapDb = _sapDbContextFactory.CreateDbContext())
					//foreach (var batch in payload.DocumentLines.Where(x => x.ItemCode == itemCode).SelectMany(x => x.BatchNumbers))
					foreach (var batch in doc.DocumentLines.Where(x => x.ItemCode == itemCode).SelectMany(x => x.BatchNumbers))
					{
						//var batchId = (from t0 in sapDb.OBTN where t0.DistNumber == batch.BatchNumber select t0.AbsEntry).Single();
						var batchId = (from t0 in sapDb.OBTN where t0.DistNumber == batch.BatchNumberProperty select t0.AbsEntry).Single();
						var request = new SLBatchRequest(
							HttpMethod.Patch, $"BatchNumberDetails({batchId})",
							JsonConvert.SerializeObject(new
							{
								U_BatchStatus = dispatchStatusStr,
								U_TIMS_DispatchDate = DateTime.Now,
								U_TIMS_DispatchTime = TimeOfDay.Now,
							}, ServiceLayerExtensions.SerializerSettings),
							contentID: batchRequests.Count + 1);
						batchRequests.Add(request);
					}
				var resps = await _sl.BatchAsync(batchRequests.ToArray());

				if (!resps.All(x => x.IsSuccessStatusCode))
				{
					var err = resps.Where(x => !x.IsSuccessStatusCode).First();
					var content = await err.Content.ReadAsStringAsync();

					var msg = JsonConvert.DeserializeObject<SLResponseError>(content);

					throw new Exception($"Posting failed {msg.Error.Message.Value}");
				}
				var batches = doc.DocumentLines.Where(x => x.ItemCode == itemCode).SelectMany(x => x.BatchNumbers).Select(x => x.BatchNumberProperty).ToHashSet();
				//var batches = payload.DocumentLines.Where(x => x.ItemCode == itemCode).SelectMany(x => x.BatchNumbers).Select(x => x.BatchNumber).ToHashSet();

				//REMOVE CLEAR OUT BIN AFTER DISPATCHING - KARL 12/11/2023
				if (!_binDataService.ClearPalletLabel(data.Pallets.Select(x => x.Code).ToList()))
				{
					throw new Exception($"Clearing Pallets Failed.");
				}


				var newModel = _mapper.Map<DispatchModel>(data);
				return newModel;
			}
			catch (Exception e) when (e is SLException || e is FlurlHttpException)
			{
				throw;
			}
		}

		public DispatchModel Get(int id)
		{
			throw new NotImplementedException();
		}

		public List<DispatchModel> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<List<DispatchModel>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<DispatchModel> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public List<SalesOrder> GetDispatchableSalesOrders()
		{
			// TODO: Filter sales order list. Only those that are ready for disptach must be shown
			string query = @"
            with lastServiceIndicator as (
	                SELECT T0.Code, MAX(T1.U_SortCode)[U_SortCode] FROM [@SERVICE_DATA] T0
	                JOIN [@SERVICE_DATA_ROW] T1 ON T0.Code = T1.Code
	                GROUP BY T0.Code
                ),
                lastServiceRow as (
	                SELECT t0.* FROM [@SERVICE_DATA_ROW] t0
	                JOIN  lastServiceIndicator t1 ON t0.Code = t1.Code AND t0.U_SortCode = t1.U_SortCode
                ),
                receivedSO as (    
                    SELECT
                    T0.DocEntry
                    FROM ORDR T0
                    JOIN lastServiceRow T1 ON T0.U_ServiceType = T1.Code AND T0.U_SOStatus = T1.U_DisplayStatus
                )

                SELECT
						T0.U_PickupDate, X0.Quantity, T1.ItemName, T1.U_NoBoxesPallet, T2.CardName, T0.* 
                    FROM ORDR T0
                    LEFT JOIN RDR1 T3 ON T0.DocEntry = T3.DocEntry
                    --LEFT JOIN OITM T1 ON T3.U_ItemCode = T1.ItemCode
                    LEFT JOIN OITM T1 ON T3.ItemCode = T1.ItemCode
					LEFT JOIN OITB TG ON T1.ItmsGrpCod = TG.ItmsGrpCod
                    LEFT JOIN OCRD T2 ON T0.CardCode = T2.CardCode
                    --JOIN receivedSO W1 ON t0.DocEntry = W1.DocEntry
                    OUTER APPLY (
                        SELECT TOP 1 S0.Quantity FROM RDR1 S0 WHERE T0.DocEntry = S0.DocEntry 
                        --AND isnull(S0.""U_ItemCode"", '') <> ''
                    ) X0
                    --WHERE T0.U_SOStatus = 'For Dispatch'
                    WHERE T0.U_SOStatus = 'For Dispatch - Ready'
                    /*AND T1.ItmsGrpCod = 110 */
					AND TG.ItmsGrpNam like '%customer items%'
                    AND T1.InvntItem = 'Y'
                    --AND isnull(T3.""U_ItemCode"", '') <> ''
                    ORDER BY T0.DocNum DESC;
                    --WHERE T0.DocDueDate = CAST( GETDATE() AS Date );
            ";
			var constr = _conf.GetConnectionString("SAP");
			var data = _mysql.GetData(query, constr, CommandType.Text);
			var res = data.Select(x => new SalesOrder
			{
				DocEntry = x.DocEntry,
				DocNum = x.DocNum,
				ItemName = x.ItemName,
				DocDate = x.DocDate,
				BpName = x.CardName,
				BoxesPallet = int.Parse(x.U_NoBoxesPallet?.ToString() ?? "0"),
				PlannedBoxNo = (double)(x.Quantity ?? 0.0)
			});
			var d = res.ToList();
			return d;
		}

		public bool AllBoxIsForDispatch(string palletCode)
		{
			using (var db = _sapDbContextFactory.CreateDbContext())
			{
				var salesOrderServiceType = (from t0 in db.OBTN
											 join t1 in db.ORDR on t0.U_SONo equals t1.DocNum
											 where t0.MnfSerial == palletCode
											 select t1.U_ServiceType).FirstOrDefault();

				var serviceType = _serviceTypeService.Get(salesOrderServiceType);
				//var releasedStatusKeyword = "For Release"; //serviceType.SERVICE_DATA_ROWCollection.Last().Code;
				var lastSortCode = serviceType.SERVICE_DATA_ROWCollection.Max(x => x.U_SortCode);

				var batchesSortCode = db.OBTN.Where(x => x.MnfSerial == palletCode).Select(x => x.U_TIMS_ITSortCode).ToList();

				bool allIsReleased = batchesSortCode.TrueForAll(x => x.Equals(lastSortCode));

				return allIsReleased;
			}
		}

		public bool AllBoxIsDispatched(string palletCode)
		{
			using (var db = _sapDbContextFactory.CreateDbContext())
			{
				var salesOrderServiceType = (from t0 in db.OBTN
											 join t1 in db.ORDR on t0.U_SONo equals t1.DocNum
											 where t0.MnfSerial == palletCode
											 select t1.U_ServiceType).FirstOrDefault();

				var serviceType = _serviceTypeService.Get(salesOrderServiceType);
				//var releasedStatusKeyword = "For Release"; //serviceType.SERVICE_DATA_ROWCollection.Last().Code;
				var lastSortCode = serviceType.SERVICE_DATA_ROWCollection.Max(x => x.U_SortCode);

				var batchesSortCode = db.OBTN.Where(x => x.MnfSerial == palletCode).Select(x => x.U_BatchStatus).ToList();

				bool allIsDispatched = batchesSortCode.TrueForAll(x => x.Equals("Dispatched"));

				return allIsDispatched;
			}
		}

		public bool AllBoxIsDispatched(params string[] palletCodes)
		{
			var p = palletCodes.AsList();
			var allIsReleased = p.TrueForAll(AllBoxIsForDispatch);
			return allIsReleased;
		}

		public bool AllBoxIsReleased(params string[] palletCodes)
		{
			var p = palletCodes.AsList();
			var allIsReleased = p.TrueForAll(AllBoxIsForDispatch);
			return allIsReleased;
		}
	}
}
