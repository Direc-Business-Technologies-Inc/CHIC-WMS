using Application.Libraries.SAP.DB.Models;
using Application.Libraries.SAP.SL;
using B1SLayer;
using DataManager.Libraries.Repositories;
using DataManager.Models;
using DataManager.Models.Enums;
using DataManager.Models.Receiving;
using DataManager.Services.Data;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Models.ViewModels.ReceivingViewModel;

namespace Application.Services.Core
{
    public class ReceivingService : Repositories.GenericService<ReceivingModel>, IReceivingService
    {
        private readonly IMySqlDataAccess _mysql;
        private readonly ISalesOrderService _salesOrderService;
        private readonly IConfiguration _conf;
        private readonly IServiceLayerDataAccess _sl;
        private readonly IUtilities _utilities;
        private readonly IDbContextFactory<Context> _contextFactory;

		public ReceivingService(IMySqlDataAccess mysql, ISalesOrderService salesOrderService, IConfiguration conf, IServiceLayerDataAccess sl, IUtilities utilities, IDbContextFactory<Context> contextFactory)
		{
			_mysql = mysql;
			_salesOrderService = salesOrderService;
			_conf = conf;
			_sl = sl;
			_utilities = utilities;
			_contextFactory = contextFactory;
		}

		public override ReceivingModel Create(ReceivingModel data)
        {
            throw new NotImplementedException();
        }

        public async override Task<ReceivingModel> CreateAsync(ReceivingModel data)
        {
            Document doc = new()
            {
                U_Remarks = "",
                U_IrridiationDate = DateTime.Now,
                U_IrridiationStart = DateTime.Now.TimeOfDay,
                U_IrridiationEnd = DateTime.Now.TimeOfDay,
                U_PickUpDate = DateTime.Now,
                U_SONo = "",
                U_ARDPNo = "",
            };

            var constr = _conf.GetConnectionString("SAP");
            string qry = "SELECT * FROM ORDR WHERE DocEntry = @entry";
            var res = _mysql.GetData<ORDR, object>(qry, new {entry = data.SalesOrderId}, constr, CommandType.Text).Single();

            doc.U_IrridiationDate = res.U_IrridiationDate;

            
            doc.U_IrridiationStart = _utilities.ToTimeOfDay(res.U_IrridiationStart.GetValueOrDefault());
            doc.U_IrridiationEnd = _utilities.ToTimeOfDay(res.U_IrridiationEnd.GetValueOrDefault());
            doc.U_PickUpDate = res.U_PickUpDate;
            doc.U_SONo = res.DocEntry.ToString();
            doc.U_ARDPNo = res.U_ARDPNo;
            doc.U_Remarks = "test";

			//string qryRdr1 = "SELECT * FROM RDR1 WHERE DocEntry = @entry and isnull(U_ItemCode, '') <> ''";
			//var resRdr1 = _mysql.GetData<RDR1, object>(qry, new { entry = data.SalesOrderId }, constr, CommandType.Text).Single();
            string itemCode = res.U_CustItems;
            //string itemCode = resRdr1.U_ItemCode;

            var item = new DocumentLine
            {
                ItemCode = itemCode,
                WarehouseCode = "RC"
            };
            doc.DocumentLines.Add(item);

            foreach(var pallet in data.Pallets)
            {
                var deets = GetPalletDetails(pallet.Label);
                for(int i=1; i<=pallet.Quantity;i++)
                {
                    BatchNumber batch = new()
                    {
                        BatchNumberProperty = string.Format("{0}-{1}", res.DocNum, (item.BatchNumbers.Count() + 1).ToString("D4")),
                        Quantity = 1,
                        ManufacturerSerialNumber = pallet.Label
                    };
                    item.BatchNumbers.Add(batch);
                }
            }

            //var reps = await _sl.PostAsync<Document, JObject>("InventoryGenEntries", jPayload);

            var payload = new
            {
                DocumentLines = doc.DocumentLines.Select(line => new
                {
                    line.ItemCode,
                    line.WarehouseCode,
                    Quantity = line.BatchNumbers.Sum(b => b.Quantity),
                    BatchNumbers = line.BatchNumbers.Select(batch => new
                    {
                        BatchNumber = batch.BatchNumberProperty,
                        Quantity = 1,
                        batch.ManufacturerSerialNumber,
                        doc.U_SONo
                        //U_BatchStatus = Status.Received_ForStorage.GetDescription(),
                        
                    }),
                }),
                doc.U_Remarks,
                U_IrridiationStart = doc.U_IrridiationStart.ToString(),
                U_IrridiationEnd = doc.U_IrridiationEnd.ToString(),
                doc.U_PickUpDate,
                doc.U_SONo,
                doc.U_ARDPNo
                //U_SOStatus = Status.Received_ForStorage.GetDescription()
            };

            try
            {
                var strPayload = JsonConvert.SerializeObject(payload);
                //var response = await _sl.PostAsync<JObject, object>("InventoryGenEntries", payload);

                SLBatchRequest req1 = new(HttpMethod.Post, "InventoryGenEntries", JsonConvert.SerializeObject(payload), contentID: 1);
                //SLBatchRequest req2 = new(HttpMethod.Patch, $"Orders({doc.U_SONo})", new
                //{
                //    U_SOStatus = Status.Received_ForStorage.GetDescription()
                //}, contentID: 2);

                var resps = await _sl.BatchAsync(new SLBatchRequest[] { req1 });

                if (!resps.All(x => x.IsSuccessStatusCode))     
                {
                    var err = resps.Where(x => !x.IsSuccessStatusCode).First();
                    var msg = await err.Content.ReadAsStringAsync();
                    throw new Exception($"Posting failed {msg}");
                }

                return new();
            } catch(Exception e) when (e is SLException || e is FlurlHttpException)
            {
                throw;
            }

        }

        public override ReceivingModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ReceivingModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<List<ReceivingModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<ReceivingModel> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<SalesOrder> GetSalesOrderList()
        {

            var z = _salesOrderService.GetAll();
            //string query = @"
            //  WITH receivedSO as (    
            //        SELECT T1.DocEntry FROM OIGN T0
            //        INNER JOIN ORDR T1 ON T0.U_SONo = T1.DocNum
            //    )
                
            //    SELECT X0.Quantity, T1.ItemName, T1.U_NoBoxesPallet, T2.CardName, T0.* FROM ORDR T0
            //        LEFT JOIN OITM T1 ON T0.U_CustItems = T1.ItemCode
            //        LEFT JOIN OCRD T2 ON T0.CardCode = T2.CardCode
            //        LEFT JOIN receivedSO W1 ON t0.DocEntry = W1.DocEntry
            //        OUTER APPLY (
            //            SELECT TOP 1 S0.Quantity FROM RDR1 S0 WHERE T0.DocEntry = S0.DocEntry
            //        ) X0
            //        WHERE W1.DocEntry IS NULL;
            //";

			string query = @"
                SELECT DISTINCT
                    X0.Quantity, 
	                T0.DocEntry,
	                T0.DocNum,
	                T0.DocDate,
                    T1.ItemName, 
                    T1.U_NoBoxesPallet, 
                    T2.CardName
                FROM 
                    ORDR T0 LEFT JOIN 
                    RDR1 T3 ON T3.DocEntry = T0.DocEntry
                    --OITM T1 ON T3.U_ItemCode = T1.ItemCode 
                    LEFT JOIN OITM T1 ON T0.U_CustItems = T1.ItemCode 
                    LEFT JOIN OITB TG ON T1.ItmsGrpCod = TG.ItmsGrpCod
                    LEFT JOIN OCRD T2 ON T0.CardCode = T2.CardCode OUTER APPLY (
                        SELECT TOP 1 
                        S0.Quantity 
                        FROM 
                        RDR1 S0 
                        WHERE 
                        T0.DocEntry = S0.DocEntry
                    ) X0
                WHERE 
                    T0.U_SOStatus = 'For Receiving - Ready'
                    /*AND T1.ItmsGrpCod = 110 */
                    AND TG.ItmsGrpNam like '%customer items%'
                    AND T1.InvntItem = 'Y'
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

            using(var db = _contextFactory.CreateDbContext())
            {
				List<SalesOrder> passedOrders = new();
                foreach(var order in res)
                {
                    var qcOrdr = db.QCOrders.FirstOrDefault(x => x.DocNo == order.DocNum);

					var isPassed = qcOrdr?.Status.ToUpper().Contains("PASSED") ?? false;
                    if (!isPassed) continue;
                    passedOrders.Add(order);
                }
                
                var twoMinutesRemainingTilTimeOut = 120;

                return passedOrders;
            }
        }
        public (string SoNum, string PalletSeries, double BoxNo) GetPalletDetails(string palletCode)
        {
            string so, pal;
            double box;

            if(string.IsNullOrEmpty(palletCode)) throw new ArgumentNullException("palletCode");

            string[] extractedData = palletCode.Split('-');
            if (extractedData.Length < 3) return (string.Empty, string.Empty, 0);

            so = extractedData[0];
            pal = extractedData[1];

            string strBoxNo = extractedData[2];
            double.TryParse(strBoxNo, out box);

            return (so, pal, box);
        }
    }
}
