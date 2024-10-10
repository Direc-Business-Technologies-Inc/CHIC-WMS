using DataManager.Models.DashboardNotification;
using static DataManager.Models.DashboardNotification.DashboardNotificationModel;

namespace DataManager.Services.Core
{
	public class DashboardNotificationDataService : GenericService<DashboardNotificationModel>, IDashboardNotificationDataService
	{
		private readonly IMySqlDataAccess _sql;
		private readonly IConfiguration _configuration;
		private readonly IDbContextFactory<Context> _contextFactory;

		public DashboardNotificationDataService(IMySqlDataAccess mySqlDataAccess, IConfiguration configuration, IDbContextFactory<Context> contextFactory)
		{
			_sql = mySqlDataAccess;
			_configuration = configuration;
			_contextFactory = contextFactory;
		}

		public override DashboardNotificationModel Create(DashboardNotificationModel data)
		{
			throw new NotImplementedException();
		}

		public override Task<DashboardNotificationModel> CreateAsync(DashboardNotificationModel data)
		{
			throw new NotImplementedException();
		}

		public override DashboardNotificationModel Get(int id)
		{
			// TODO: needs optimization. query for selecting single doc only
			var d = GetAll().FirstOrDefault(x => x.DocNum == id);

			if (d == null)
			{
				d = new DashboardNotificationModel();
				d.DocNum = id;
				d.Status = "";
				d.StorageConditions = "";
			}

			return d;
		}

		public override List<DashboardNotificationModel> GetAll()
		{
			//string query = """
			//    /*Dashboard*/
			//    select 
			//    	T0.DocNum
			//    	, T0.U_SOStatus as [SO_Status]
			//    	, isnull(X0.MnfSerial,'') as PalletNo
			//    	, isnull(X0.U_BatchStatus,'') as CurrentStatus
			//    	, isnull(X0.U_TIMS_OngoingStatus, '') as OngoingStatus
			//    from ORDR T0 	
			//    	outer apply (select distinct T1.MnfSerial, T1.U_BatchStatus , T1.U_TIMS_OngoingStatus
			//    				from OBTN T1 
			//    				inner join [@SERVICE_DATA_ROW] T2 on T1.U_BatchStatus=T2.U_DisplayStatus
			//    				where isnull(T1.U_SONo,0) = T0.DocNum) X0
			//    where T0.DocStatus ='O'
			//    """;
			//string query = """
			//             /*Dashboard*/
			//             WITH receivedSO as (    
			//                   SELECT T1.DocEntry FROM OIGN T0
			//                   INNER JOIN ORDR T1 ON T0.U_SONo = T1.DocNum
			//               )

			//             SELECT 'Receiving' "Status", T0.DocNum, T1.U_TIMS_StorCon "StorageConditions" FROM ORDR T0
			//                 LEFT JOIN OITM T1 ON T0.U_CustItems = T1.ItemCode
			//                 LEFT JOIN receivedSO W1 ON t0.DocEntry = W1.DocEntry
			//             WHERE W1.DocEntry IS NULL

			//             UNION ALL

			//             SELECT 'Dispatch' "Status", T0.DocNum, T1.U_TIMS_StorCon "StorageConditions" FROM ORDR T0
			//                 LEFT JOIN OITM T1 ON T0.U_CustItems = T1.ItemCode
			//             WHERE T0.U_SOStatus = 'For Dispatch'
			//             ORDER BY "Status", DocNum ASC;
			//             """;

			//string query = """
			//             /*Dashboard*/

			//             SELECT 
			//             	'Receiving' "Status",
			//             	T0.DocNum,
			//             	T1.U_TIMS_StorCon "StorageConditions"
			//             FROM 
			//             	ORDR T0 LEFT JOIN RDR1 T2 ON T2.DocEntry = T0.DocEntry LEFT JOIN 
			//             	OITM T1 ON T2.U_ItemCode = T1.ItemCode
			//             	--OITM T1 ON T0.U_CustItems = T2.ItemCode
			//             WHERE 
			//             	(SELECT COUNT(x."DocEntry") "Count" FROM OIGN x WHERE x.U_SONo = T0."DocNum") = 0 and isnull(T2.""U_ItemCode"", '') <> ''

			//             --UNION ALL 

			//             --SELECT 
			//             --	'Receiving' "Status",
			//             --	T0.DocNum,
			//             --	T1.U_TIMS_StorCon "StorageConditions"
			//             --FROM 
			//             --	ORDR T0 LEFT JOIN 
			//             --	OITM T1 ON T0.U_CustItems = T1.ItemCode INNER JOIN
			//             --	OIGN T3 ON T3.U_SONo = T0.DocNum
			//             --WHERE 
			//             --	 T3.U_Remarks = 'test1' 


			//             UNION ALL

			//             SELECT 'Dispatch' "Status", T0.DocNum, T1.U_TIMS_StorCon "StorageConditions" FROM ORDR T0 LEFT JOIN RDR1 T2 ON T2.DocEntry = T0.DocEntry
			//                 LEFT JOIN OITM T1 ON T2.U_ItemCode = T1.ItemCode
			//                 --LEFT JOIN OITM T1 ON T0.U_CustItems = T1.ItemCode
			//             WHERE T0.U_SOStatus = 'For Dispatch' and isnull(T2.""U_ItemCode"", '') <> ''
			//             ORDER BY "Status", DocNum ASC;
			//             """;

			string query = @"
				/*Dashboard*/
    
				SELECT DISTINCT
								'Receiving' AS Status,
								T0.DocEntry,
								T0.DocNum,
								T0.DocDueDate AS DispatchDate,
								T0.U_EBStatus AS EBStatus, 
								T2.Quantity AS SOQUantity,
								T1.U_TIMS_StorCon AS StorageConditions,
								CASE 
									WHEN LEN(T0.U_IrridiationStart) <= 4 
										THEN
										DATEADD(SECOND, FLOOR((T0.U_IrridiationStart / 100) * 60) * 60 + (T0.U_IrridiationStart % 100) * 60, T0.U_IrridiationDate) 
									WHEN LEN(T0.U_IrridiationStart) > 4 
										THEN 
										DATEADD(SECOND, FLOOR(T0.U_IrridiationStart / 1000) * 3600 + 
										FLOOR(((T0.U_IrridiationStart % 1000) / 100) * 60) * 60 + (T0.U_IrridiationStart % 100) * 60, T0.U_IrridiationDate) 
								END AS IrradiationDate,
								T1.U_TIMS_BeamEnergy AS BeamEnergy,
								T1.U_TIMS_BeamPower AS BeamPower,
								T1.U_TIMS_Frequency AS Frequency
				FROM ORDR T0 
								LEFT JOIN RDR1 T2 ON T2.DocEntry = T0.DocEntry 
								LEFT JOIN OITM T1 ON T2.ItemCode = T2.ItemCode
								LEFT JOIN OITB TG ON T1.ItmsGrpCod = TG.ItmsGrpCod
				WHERE 
								(SELECT COUNT(x.DocEntry) AS Count FROM OIGN x WHERE x.U_SONo = T0.DocNum) = 0 
								--((SELECT COUNT(x.AbsEntry) FROM OBTN x WHERE x.U_SONo = T0.DocNum) 
								--= (SELECT COUNT(x.AbsEntry) FROM OBTN x WHERE x.U_SONo = T0.DocNum AND x.U_TIMS_ITSortCode = 1))
								/*AND T1.ItmsGrpCod = 110 */
								AND TG.ItmsGrpNam like '%customer items%'
								AND T1.InvntItem = 'Y'
				UNION ALL

				SELECT DISTINCT 
								T0.U_SOStatus AS Status, 
								T0.DocEntry,
								T0.DocNum, 
								T0.DocDueDate AS DispatchDate,
								T0.U_EBStatus AS EBStatus, 
								T2.Quantity AS SOQUantity,
								T1.U_TIMS_StorCon AS StorageConditions,
								CASE 
									WHEN LEN(T0.U_IrridiationStart) <= 4 
										THEN
										DATEADD(SECOND, FLOOR((T0.U_IrridiationStart / 100) * 60) * 60 + (T0.U_IrridiationStart % 100) * 60, T0.U_IrridiationDate) 
									WHEN LEN(T0.U_IrridiationStart) > 4 
										THEN 
										DATEADD(SECOND, FLOOR(T0.U_IrridiationStart / 1000) * 3600 + 
										FLOOR(((T0.U_IrridiationStart % 1000) / 100) * 60) * 60 + (T0.U_IrridiationStart % 100) * 60, T0.U_IrridiationDate) 
								END AS IrradiationDate,
								T1.U_TIMS_BeamEnergy AS BeamEnergy,
								T1.U_TIMS_BeamPower AS BeamPower,
								T1.U_TIMS_Frequency AS Frequency
				FROM ORDR T0 
								LEFT JOIN RDR1 T2 ON T2.DocEntry = T0.DocEntry
								LEFT JOIN OITM T1 ON T2.ItemCode = T1.ItemCode
								LEFT JOIN OITB TG ON T1.ItmsGrpCod = TG.ItmsGrpCod
				WHERE ISNULL(T0.U_SOStatus,'') <> ''
								AND T0.U_SOStatus <> 'Dispatched'
								/*AND T1.ItmsGrpCod = 110 */
								AND TG.ItmsGrpNam like '%customer items%'
								AND T1.InvntItem = 'Y'
								OR ((SELECT COUNT(x.DocEntry) AS Count FROM OIGN x WHERE x.U_SONo = T0.DocNum) = 0 AND T0.U_SOStatus <> 'For Receiving - Ready')
				ORDER BY Status, DocNum ASC;
			";


			var constr = _configuration.GetConnectionString("SAP");
			var result = _sql.GetData(query, constr, System.Data.CommandType.Text);

			using (var db = _contextFactory.CreateDbContext())
			{
				Dictionary<int, DashboardNotificationModel> data = new();
				foreach (var row in result)
				{
					DashboardNotificationModel target;
					if (!data.TryGetValue(row.DocNum, out target))
					{

						target = new()
						{
							DocEntry = row.DocEntry,
							DocNum = row.DocNum,
							Status = row.Status,
							StorageConditions = row.StorageConditions,
							EBStatus = row.EBStatus,
							IrradiationDate = row.IrradiationDate?.ToString() ?? "",
							DispatchDate = row.DispatchDate?.ToString() ?? "",
							BeamEnergy = row.BeamEnergy?.ToString() ?? "0",
							BeamPower = row.BeamPower?.ToString() ?? "0",
							Frequency = row.Frequency?.ToString() ?? "0",
							SOQUantity = Convert.ToInt32(row.SOQUantity ?? 0)
							//Status = row.SO_Status
						};

						//if (row.Status == "For Receiving - Ready")
						//{
						//	var qcOrdr = db.QCOrders.FirstOrDefault(x => x.DocNo == target.DocNum);

						//	var isPassed = qcOrdr?.Status.ToUpper().Contains("PASSED") ?? false;
						//	if (!isPassed) continue;
						//}
						data.Add(target.DocNum, target);
					}

					string DocNum = row.DocNum.ToString();

					query = @$"
								SELECT DISTINCT
									MnfSerial as PalletNo,
									U_TIMS_OngoingStatus as OngoingStatus
								FROM 
									OBTN
								WHERE
									U_SONo = '{DocNum}'
							";

					var PalletList = _sql.GetData(query, constr, System.Data.CommandType.Text);

					//ADD BINCODE FOR EACH PALLETS
					var bins = db.BinAssignments.Where(x => x.BinCode != "" && x.SONo != "" && x.SONo != "0").ToList();

					foreach (var palletLine in PalletList)
					{
						DashboardNotificationModel.DashboardNotificationLineModel line = new()
						{
							PalletNo = palletLine.PalletNo,
							BinCode = bins.Where(x => x.PalletNo == palletLine.PalletNo)?.FirstOrDefault()?.BinCode ?? "",
							//Status = row.CurrentStatus,
							OngoingStatus = palletLine.OngoingStatus == null ? "" : palletLine.OngoingStatus.ToString(),
						};
						if (string.IsNullOrEmpty(line.PalletNo)) continue;
						target.Lines.Add(line);
					}
				}

				return data.Values.ToList();
			}
		}

		public override Task<List<DashboardNotificationModel>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public override Task<DashboardNotificationModel> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateStatusSO(DashboardNotificationModel Item, string Status)
		{
			//var requests = new List<SLBatchRequest>();

			//var Order = new Document
			//{
			//	U_SOStatus = Status
			//};

			//SLBatchRequest orderStatusUpdate = new(HttpMethod.Patch, $"Orders({Item.DocEntry})", Serialize(Order), contentID: requests.Count + 1);
			//requests.Add(orderStatusUpdate);

			//var resps = await _sl.BatchAsync(requests.ToArray());
			//var errors = resps.Where(x => !x.IsSuccessStatusCode);
			//if (errors.Any())
			//{
			//	var resp = errors.First();
			//	var error = await resp.Content.ReadAsStringAsync();
			//	throw new Exception(error);
			//}

			return true;
		}

		public async Task<bool> UpdateEBStatusSO(DashboardNotificationModel Item, string Status)
		{
			//var requests = new List<SLBatchRequest>();

			//var Order = new Document
			//{
			//	U_EBStatus = Status
			//};

			//SLBatchRequest orderStatusUpdate = new(HttpMethod.Patch, $"Orders({Item.DocEntry})", Serialize(Order), contentID: requests.Count + 1);
			//requests.Add(orderStatusUpdate);

			//var resps = await _sl.BatchAsync(requests.ToArray());
			//var errors = resps.Where(x => !x.IsSuccessStatusCode);
			//if (errors.Any())
			//{
			//	var resp = errors.First();
			//	var error = await resp.Content.ReadAsStringAsync();
			//	throw new Exception(error);
			//}

			return true;
		}

		public async Task<bool> UpdateEBStatusPallet(DashboardNotificationLineModel Item, string Status)
		{
			//var requests = new List<SLBatchRequest>();

			////Get all batches with same pallet no.
			//string query = @$"
			//		SELECT 
			//			AbsEntry
			//		FROM
			//			OBTN
			//		WHERE
			//			MnfSerial = '{Item.PalletNo}'
			//	";

			//var constr = _configuration.GetConnectionString("SAP");
			//var PalletList = _sql.GetData(query, constr, System.Data.CommandType.Text);

			//foreach (var palletLine in PalletList)
			//{
			//	var batchPayload = new BatchNumberDetail
			//	{
			//		U_TIMS_OngoingStatus = Status
			//	};

			//	SLBatchRequest batchStatusUpdate = new(HttpMethod.Patch, $"BatchNumberDetails({palletLine.AbsEntry})", Serialize(batchPayload), contentID: requests.Count + 1);
			//	requests.Add(batchStatusUpdate);
			//}

			//var resps = await _sl.BatchAsync(requests.ToArray());
			//var errors = resps.Where(x => !x.IsSuccessStatusCode);
			//if (errors.Any())
			//{
			//	var resp = errors.First();
			//	var error = await resp.Content.ReadAsStringAsync();
			//	throw new Exception(error);
			//}

			return true;
		}

		//private string Serialize(object data) => JsonConvert.SerializeObject(data, ServiceLayerExtensions.SerializerSettings);

		public async Task<List<DashboardNotificationModel>> GetAllSODashboardMobile()
		{
			string query = @"
				/*Dashboard*/
    
				SELECT DISTINCT 
					T0.U_SOStatus AS Status, 
					T0.DocEntry,
					T0.DocNum, 
					T0.U_EBStatus AS EBStatus, 
					T2.Quantity AS SOQUantity,
					T1.U_TIMS_StorCon AS StorageConditions,
					CASE 
									WHEN LEN(T0.U_IrridiationStart) <= 4 
										THEN
										DATEADD(SECOND, FLOOR((T0.U_IrridiationStart / 100) * 60) * 60 + (T0.U_IrridiationStart % 100) * 60, T0.U_IrridiationDate) 
									WHEN LEN(T0.U_IrridiationStart) > 4 
										THEN 
										DATEADD(SECOND, FLOOR(T0.U_IrridiationStart / 1000) * 3600 + 
										FLOOR(((T0.U_IrridiationStart % 1000) / 100) * 60) * 60 + (T0.U_IrridiationStart % 100) * 60, T0.U_IrridiationDate) 
					END AS IrradiationDate,
					T1.U_TIMS_BeamEnergy AS BeamEnergy,
					T1.U_TIMS_BeamPower AS BeamPower,
					T1.U_TIMS_Frequency AS Frequency
					FROM ORDR T0 
												LEFT JOIN RDR1 T2 ON T2.DocEntry = T0.DocEntry
												LEFT JOIN OITM T1 ON T0.U_CustItems = T1.ItemCode
												LEFT JOIN OITB TG ON T1.ItmsGrpCod = TG.ItmsGrpCod
					WHERE ISNULL(T0.U_SOStatus,'') <> ''
												AND T0.U_SOStatus NOT LIKE '%Dispatch%'
												
												AND TG.ItmsGrpNam like '%customer items%'
												AND T1.InvntItem = 'Y'
												AND (SELECT TOP 1 ISNULL(X.DocNum, '') FROM OIGN X WHERE X.U_SONo = T0.DocEntry) <> ''
												--AND (SELECT TOP 1 ISNULL(X.DocNum, '') FROM ODLN X WHERE X.U_SONo = T0.DocEntry) = ''
					ORDER BY DocNum desc;
			";


			var constr = _configuration.GetConnectionString("SAP");
			var result = _sql.GetData(query, constr, System.Data.CommandType.Text);

			using (var db = _contextFactory.CreateDbContext())
			{
				Dictionary<int, DashboardNotificationModel> data = new();
				foreach (var row in result)
				{
					DashboardNotificationModel target;
					if (!data.TryGetValue(row.DocNum, out target))
					{

						target = new()
						{
							DocEntry = row.DocEntry,
							DocNum = row.DocNum,
							Status = row.Status,
							StorageConditions = row.StorageConditions,
							EBStatus = row.EBStatus,
							IrradiationDate = row.IrradiationDate?.ToString() ?? "",
							BeamEnergy = row.BeamEnergy?.ToString() ?? "0",
							BeamPower = row.BeamPower?.ToString() ?? "0",
							Frequency = row.Frequency?.ToString() ?? "0",
							SOQUantity = Convert.ToInt32(row.SOQUantity ?? 0)
							//Status = row.SO_Status
						};

						if (row.Status == "Receiving")
						{
							var qcOrdr = db.QCOrders.FirstOrDefault(x => x.DocNo == target.DocNum);

							var isPassed = qcOrdr?.Status.ToUpper().Contains("PASSED") ?? false;
							if (!isPassed) continue;
						}
						data.Add(target.DocNum, target);
					}

					string DocNum = row.DocNum.ToString();

					query = @$"
								SELECT DISTINCT
									MnfSerial as PalletNo,
									U_TIMS_OngoingStatus as OngoingStatus
								FROM 
									OBTN
								WHERE
									U_SONo = '{DocNum}'
							";

					var PalletList = _sql.GetData(query, constr, System.Data.CommandType.Text);

					//ADD BINCODE FOR EACH PALLETS
					var bins = db.BinAssignments.Where(x => x.BinCode != "" && x.SONo != "" && x.SONo != "0").ToList();

					foreach (var palletLine in PalletList)
					{
						DashboardNotificationModel.DashboardNotificationLineModel line = new()
						{
							PalletNo = palletLine.PalletNo,
							BinCode = bins.Where(x => x.PalletNo == palletLine.PalletNo)?.FirstOrDefault()?.BinCode ?? "",
							//Status = row.CurrentStatus,
							OngoingStatus = palletLine.OngoingStatus == null ? "" : palletLine.OngoingStatus.ToString(),
						};
						if (string.IsNullOrEmpty(line.PalletNo)) continue;
						target.Lines.Add(line);
					}
				}

				return data.Values.ToList();
			}
		}
	}
}
