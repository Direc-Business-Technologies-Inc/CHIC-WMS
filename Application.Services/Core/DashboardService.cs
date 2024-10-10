using Application.Libraries.SAP;
using Application.Libraries.SAP.SL;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using static Application.Models.ViewModels.DashboardViewModel;

namespace Application.Services.Core;

public class DashboardService : IDashboardService
{
	private readonly IMapper Mapper;
	private readonly IMsSqlDataAccess _sql;
	private readonly IBinDataServices _binDataServices;
	private readonly IDbContextFactory<SapDb> _sapDbFactory;
	private readonly IServiceTypeService _serviceTypeService;
	private readonly IConfiguration _configuration;
	public DashboardService(IConfiguration configuration, IMapper mapper, IBinDataServices binDataServices, IDbContextFactory<SapDb> sapDbFactory, IServiceTypeService serviceTypeService)
	{
		_sql = new MsSqlDataAccess(configuration);
		Mapper = mapper;
		_binDataServices = binDataServices;
		_sapDbFactory = sapDbFactory;
		_serviceTypeService = serviceTypeService;
		_configuration = configuration;

	}

	#region Bin Dashboard
	public async Task<DashboardViewModel.BinDashboardViewModel> InitalizeBinDashboard()
	{
		try
		{
			DashboardViewModel.BinDashboardViewModel model = new DashboardViewModel.BinDashboardViewModel();

			string qry = $@"select ""WhsCode"" as ""WarehouseCode"", ""WhsName"" as ""WarehouseName"" from OWHS WHERE ""BinActivat"" = 'Y'";

			model.WarehouseList = _sql.GetData<DashboardViewModel.BinDashboardViewModel.Warehouse, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			model.BinMappingDetails.WarehouseCode = model.WarehouseList.FirstOrDefault().WarehouseCode;
			model.BinMappingDetails.WarehouseName = model.WarehouseList.FirstOrDefault().WarehouseName;

			GetBinMappingDetails(model);

			return model;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public void GetBinMappingDetails(DashboardViewModel.BinDashboardViewModel model)
	{
		try
		{
			string qry = $@"SELECT DISTINCT T0.[SL1Code] as ""ShelfName""
					FROM OBIN T0 
					WHERE T0.WhsCode = '{model.BinMappingDetails.WarehouseCode}'
					AND T0.[SL1Code] <> 'SYSTEM-BIN-LOCATION'";

			model.Shelves = _sql.GetData<DashboardViewModel.BinDashboardViewModel.Shelf, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			if (model.BinMappingDetails.Shelf == null || !model.Shelves.Where(x => x.ShelfName == model.BinMappingDetails.Shelf).Any())
			{
				model.BinMappingDetails.Shelf = model.Shelves.FirstOrDefault().ShelfName;
			}

			BinMapping binMappings = _binDataServices.GetBinMapping(model.BinMappingDetails.WarehouseCode, model.BinMappingDetails.Shelf);

			model.BinMappingDetails = Mapper.Map<DashboardViewModel.BinDashboardViewModel.BinMapping>(binMappings);
			model.BinMappingDetails.ImageUrl = $"FILE_UPLOAD/{model.BinMappingDetails.FileName}";

			List<BinAssignment> occupiedBinAssignments = _binDataServices.GetOccupiedPalletLabel();

			foreach (var pin in model.BinMappingDetails.BinMappingPins)
			{
				pin.Status = occupiedBinAssignments.Where(x => x.BinCode == pin.BinCode && x.SONo != "" && x.SONo != "0").Any() ? "Occupied" : "Available";
				pin.SONo = occupiedBinAssignments.Where(x => x.BinCode == pin.BinCode).Any() ? occupiedBinAssignments.Where(x => x.BinCode == pin.BinCode).FirstOrDefault().SONo : "";
				pin.PalletNo = occupiedBinAssignments.Where(x => x.BinCode == pin.BinCode).Any() ? occupiedBinAssignments.Where(x => x.BinCode == pin.BinCode).FirstOrDefault().PalletNo : "";
			}

			string SOFilter = string.Join(", ", model.BinMappingDetails.BinMappingPins.Where(x => x.Status == "Occupied").Select(vm => vm.SONo).Distinct());

			qry = $@"select DISTINCT
								A.""DocEntry"" as ""SONo""
								, A.""CardName"" as ""CustomerName""
								, A.""U_IrridiationDate"" as ""IrradiationDate""
								, A.""U_PickUpDate"" as ""DispatchDate""
								, A.""DocDueDate"" as ""ReceivingDate""
								, C.""ItemName""
								, C.""U_NoBoxesPallet"" as ""NoOfBoxesPerPallet""
				from ""ORDR"" A 
				inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
				--inner join ""OITM"" C on B.""U_ItemCode"" = C.""ItemCode""
				inner join ""OITM"" C on A.""U_CustItems"" = C.""ItemCode""
				LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
				where A.""DocEntry"" in({(string.IsNullOrWhiteSpace(SOFilter) ? "''" : SOFilter)})
				/*AND C.""ItmsGrpCod"" = 110 */
				AND TG.ItmsGrpNam like '%customer items%'
				AND C.""InvntItem"" = 'Y'
				--and isnull(B.""U_ItemCode"", '') <> ''
				";

			var SalesOrders = _sql.GetData<DashboardViewModel.BinDashboardViewModel.SalesOrders, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			foreach (var pin in model.BinMappingDetails.BinMappingPins.Where(x => x.Status == "Occupied"))
			{
				var salesOrder = SalesOrders.Where(x => x.SONo.ToString() == pin.SONo).FirstOrDefault();

				pin.CustomerName = salesOrder?.CustomerName ?? "";
				pin.ItemName = salesOrder?.ItemName ?? "";
				pin.NoOfBoxesPerPallet = salesOrder?.NoOfBoxesPerPallet.ToString() ?? "";
				pin.ReceivingDate = salesOrder?.ReceivingDate.ToString("yyyy-MM-dd") ?? "";
				pin.IrradiationDate = salesOrder?.IrradiationDate.ToString("yyyy-MM-dd") ?? "";
				pin.DispatchDate = salesOrder?.DispatchDate.ToString("yyyy-MM-dd") ?? "";
			}
		}
		catch (Exception)
		{
			//throw;
		}
	}
	#endregion

	#region Sales Order Details
	public async Task<DashboardViewModel> InitializeSalesOrderDetails(string SONo)
	{
		DashboardViewModel model = new DashboardViewModel();

		model.QuantitySpecificationList = GetSpecifications(SONo);
		model.ScheduleList = GetSchedules(SONo);
		model.IrradiationParameters = GetIrradiationParameters(SONo);
		//model.BatchStatuses = GetBatchDetails(SONo);
		model.SalesOrderActivities = GetSalesOrderActivities(SONo);
		model.PalletDetailsList = GetPalletDetailsList(SONo);
		model.Batches = GetBatches(SONo);

		return model;
	}

	public List<QuantitySpecs> GetSpecifications(string SONo)
	{
		string qry = $@"
				SELECT DISTINCT
					T0.DocNum,
					T3.SWeight1 AS WeightBox,
					T1.Quantity AS NoofBoxes,
					T4.NonConformity,
					(T1.Quantity * T3.SWeight1) AS TotalWeight,
					T3.U_NoBoxesPallet AS NoBoxesPallet,
					CEILING(T1.Quantity / T3.U_NoBoxesPallet) AS NoPallet,
					T3.SLength1 AS BoxLength,
					T3.SWidth1 AS BoxWidth,
					T3.SHeight1 AS BoxHeight,
					T3.SVolume AS BoxVolume,
					T3.U_Density AS Density
				FROM ORDR T0
				JOIN RDR1 T1 ON T0.DocEntry = T1.DocEntry
				--JOIN OITM T3 ON T1.U_ItemCode = T3.ItemCode
				JOIN OITM T3 ON T0.U_CustItems = T3.ItemCode
				LEFT JOIN OITB TG ON T3.ItmsGrpCod = TG.ItmsGrpCod
				CROSS APPLY (
					SELECT Count(A.MnfSerial) AS NonConformity
					FROM OBTN A
					WHERE A.U_SONo = {SONo} 
					AND A.ItemCode = T3.ItemCode
					AND ISNULL(U_TIMS_ITSortCode, 0) = -1
				) T4
				WHERE T0.DocEntry = {SONo}
				/*AND T3.ItmsGrpCod = 110 */
				AND TG.ItmsGrpNam like '%customer items%'
				AND T3.InvntItem = 'Y'
				--AND ISNULL(T1.U_ItemCode, '') <> ''
			";

		//		string qry = $"""
		//	SELECT 
		//	T0.DocNum
		//	, T3.U_BoxWeight[WeightBox]
		//	, T1.Quantity[NoofBoxes]
		//	, T4.[NonConformity]
		//	, (T1.Quantity * T3.U_BoxWeight)[TotalWeight]
		//	, T3.U_NoBoxesPallet[NoBoxesPallet]
		//	, CEILING(T1.Quantity / T3.U_NoBoxesPallet)[NoPallet]
		//	, T3.U_BoxLength[BoxLength]
		//	, T3.U_BoxWidth[BoxWidth]
		//	, T3.U_BoxHeight[BoxHeight]
		//	, T3.U_BoxVol[BoxVolume]
		//	, T3.U_Density[Density]
		//	FROM ORDR T0
		//	JOIN RDR1 T1 ON T0.DocEntry = T1.DocEntry
		//	JOIN OITM T3 ON T1.U_ItemCode = T3.ItemCode
		//	--JOIN OITM T3 ON T0.U_CustItems = T3.ItemCode
		//	CROSS APPLY (SELECT Count(A.MnfSerial)[NonConformity]
		//		FROM OBTN A
		//		WHERE A.U_SONo= {SONo} 
		//		AND A.ItemCode = T3.ItemCode
		//		AND isnull(U_TIMS_ITSortCode,0) = -1
		//		) T4
		//	WHERE T0.DocEntry = {SONo}
		//	and isnull(T1.""U_ItemCode"", '') <> ''
		//""";

		var constr = _sql.GetConnection("SAP");
		var result = _sql.GetData<QuantitySpecs, object>(qry, null, constr, CommandType.Text);

		return result;
	}

	public List<Schedule> GetSchedules(string SONo)
	{

		using (var db = _sapDbFactory.CreateDbContext())
		{
			List<Schedule> result;
			var dbResult = from t0 in db.ORDR
						   where t0.DocEntry == Convert.ToInt32(SONo)
						   select new Schedule
						   {
							   DocNum = t0.DocNum,
							   DeliveryDate = t0.DocDueDate,
							   IrradiationDate = t0.U_IrridiationDate,
							   PickupReleaseDate = t0.U_PickUpDate
						   };
			result = dbResult.ToList();
			return result;
		}
	}

	public List<IrradiationParameter> GetIrradiationParameters(string SONo)
	{
		string ItemCode = "";
		string Itemqry = $@"
			Select DISTINCT B.ItemCode From	ORDR A
			INNER JOIN RDR1 B On A.DocEntry = B.DocEntry
			INNER JOIN OITM C On C.ItemCode = B.ItemCode
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
			Where A.DocEntry = {SONo} AND TG.ItmsGrpNam like '%customer items%' AND C.InvntItem = 'Y'
			";
		var Itemconstr = _sql.GetConnection("SAP");
		var ItemResult = _sql.GetData<SalesOrders, object>(Itemqry, null, Itemconstr, CommandType.Text);
		ItemCode = ItemResult.FirstOrDefault()?.ItemCode ?? "";

		var sapDB = _configuration.GetSection("SapServiceLayer")["CompanyDB"]; ;

		string qry = $"""
			with piv as (
			SELECT ItemCode, U_TIMS_BeamEnergy [EB Frequency], U_TIMS_ConvSpeed [Conveyor Speed]
			FROM {sapDB}.dbo.OITM
			WHERE ItemCode = '{ItemCode}'
		),
		dos as (
		SELECT 
		B.ItemCode,
		A.MinValue, 
		A.MaxValue 
		FROM CIP1 A
		INNER JOIN QCIP B
		ON A.InspectionPlanCode = B.InspectionPlanCode
		WHERE (LOWER(Parameter) like '%dose%' 
		OR LOWER(Parameter) like '%dosimetry%' 
		OR LOWER(Parameter) like '%dosimeter%')
		AND B.ItemCode = '{ItemCode}'
		),
		viewmodel as (
		SELECT 
		--t0.InspectionPlanCode, 
		t0.[EB Frequency][{nameof(IrradiationParameter.Energy)}],  
		t0.[Conveyor Speed][{nameof(IrradiationParameter.ConveyorSpeed)}],
		MAX(t1.MaxValue)[{nameof(IrradiationParameter.MaxDose)}],
		MAX(t1.MinValue)[{nameof(IrradiationParameter.MinDose)}]
		FROM piv t0
		JOIN dos t1 ON t0.ItemCode COLLATE SQL_Latin1_General_CP1_CI_AS = t1.ItemCode COLLATE SQL_Latin1_General_CP1_CI_AS
		GROUP BY t0.ItemCode,
		t0.[EB Frequency],
		t0.[Conveyor Speed]
		)
		SELECT t0.*, '{SONo}' as [{nameof(IrradiationParameter.DocNum)}] FROM viewmodel t0
		--SELECT t0.*, X0.DocNo[{nameof(IrradiationParameter.DocNum)}] FROM viewmodel t0
		--OUTER APPLY (
		--	SELECT TOP 1 Docno FROM QCOR WHERE t0.InspectionPlanCode = InspectionPlanCode
		--) X0
		""";
		var constr = _sql.GetConnection("CommonDb");
		var result = _sql.GetData<IrradiationParameter, object>(qry, null, constr, CommandType.Text);
		return result;
	}
	//public List<BatchStatus> GetBatchDetails(string soNo)
	//{
	//	string oitlqry = $"""
	//	SELECT T1.MdAbsEntry, T2.DistNumber[], MAX(T2.U_BatchStatus)[{nameof(BatchStatus.Activity)}]
	//	, MIN( DATEADD(MINUTE, T0.CreateTime%100, DATEADD(HOUR, T0.CreateTime/100, T0.CreateDate)) )[{nameof(BatchStatus.Start)}]
	//	, MAX( DATEADD(MINUTE, T0.CreateTime%100, DATEADD(HOUR, T0.CreateTime/100, T0.CreateDate)) )[{nameof(BatchStatus.End)}]
	//	FROM OITL T0
	//	JOIN ITL1 T1 ON T0.LogEntry = T1.LogEntry
	//	JOIN OBTN T2 ON T1.MdAbsEntry = T2.AbsEntry
	//	WHERE T0.DocType = 67 
	//	AND T2.U_SONo = 189 -- for checking only
	//	GROUP BY T1.MdAbsEntry, T2.DistNumber
	//	""";

	//	string qry = $"""
	//	with result as (
	//	    SELECT 
	//	    U_SONo, 
	//	    DistNumber,
	//	    Activity, 
	//	    ActivityDate
	//	    FROM (
	//	        SELECT 
	//	            U_SONo,
	//	            DistNumber, 
	//	            DATEADD(HOUR, U_TIMS_ITBinDispTime/100, DATEADD(MINUTE, U_TIMS_ITBinDispTime%100, U_TIMS_ITBinDispDate))[U_TIMS_ITBinDispDate],
	//	            DATEADD(HOUR, U_TIMS_ITBinILBTime/100, DATEADD(MINUTE, U_TIMS_ITBinILBTime%100, U_TIMS_ITBinILB))[U_TIMS_ITBinILB],
	//	            DATEADD(HOUR, U_TIMS_ITRecBinTime/100, DATEADD(MINUTE, U_TIMS_ITRecBinTime%100, U_TIMS_ITRecBin))[U_TIMS_ITRecBin],
	//	            DATEADD(HOUR, U_TIMS_IrradLoadTime/100, DATEADD(MINUTE, U_TIMS_IrradLoadTime%100, U_TIMS_IrradLoading))[U_TIMS_IrradLoading],
	//	            DATEADD(HOUR, U_TIMS_IUBBinTime/100, DATEADD(MINUTE, U_TIMS_IUBBinTime%100, U_TIMS_IUBBin))[U_TIMS_IUBBin],
	//	            DATEADD(HOUR, U_TIMS_IUBTime/100, DATEADD(MINUTE, U_TIMS_IUBTime%100, U_TIMS_IrradUnloading))[U_TIMS_IrradUnloading]
	//	        FROM OBTN
	//			WHERE U_SONo = @soNo
	//	    ) p  
	//	    UNPIVOT  
	//	       (ActivityDate FOR Activity IN   
	//	          (
	//	          U_TIMS_ITBinDispDate,
	//	          U_TIMS_ITBinILB,
	//	          U_TIMS_ITRecBin,
	//	          U_TIMS_IrradLoading,
	//	          U_TIMS_IUBBin,
	//	          U_TIMS_IrradUnloading
	//	          )  
	//	    )AS unpvt
	//	)
	//	SELECT DistNumber, 
	//		Activity[UdfField], 
	//		Activity[Activity], 
	//		MIN(ActivityDate)[Start], 
	//		MAX(ActivityDate)[End] FROM result
	//	GROUP BY DistNumber, Activity
	//	""";

	//	var constr = _sql.GetConnection("SAP");
	//	var resp = _sql.GetData<BatchStatus, object>(qry, new { soNo }, constr, CommandType.Text);

	//	using (var db = _sapDbFactory.CreateDbContext())
	//	{
	//		var refDoc = db.ORDR.FirstOrDefault(x => x.DocNum == int.Parse(soNo));
	//		var serviceTypeEntry = refDoc.U_ServiceType;
	//		var serviceType = _serviceTypeService.Get(serviceTypeEntry);

	//		var servicerow = serviceType.SERVICE_DATA_ROWCollection;
	//		foreach(var batch in resp.GroupBy(x=>x.DistNumber))
	//		for (int i=0; i< servicerow.Count; i++)
	//		{
	//			var nextIndex = i + 1;
	//			var row = servicerow[i];
	//			var udf = udfMapping[row.Code][row.U_TransferType];

	//			var startRef = batch.FirstOrDefault(x => x.UdfField == udf);
	//			if (startRef is null) continue;
	//			startRef.Activity = row.U_TransferType;
	//			if (nextIndex >= servicerow.Count)
	//			{
	//				continue;
	//			}

	//			var nextRow = servicerow[nextIndex];
	//			var nextRowudf = udfMapping[nextRow.Code][nextRow.U_TransferType];
	//			var endRef = batch.FirstOrDefault(x => x.UdfField == nextRowudf);

	//			if (endRef is null) 
	//				startRef.End = null;
	//			else
	//				startRef.End = endRef.Start;
	//		}
	//	}

	//	return resp;
	//}

	public List<SalesOrderActivity> GetSalesOrderActivities(string SONo)
	{
		string qry = $"""
			with result as (
		    SELECT 
		        U_SONo,
		        DistNumber, 
				MnfSerial,
				DATEADD(HOUR, U_TIMS_DispatchTime/100, DATEADD(MINUTE, U_TIMS_DispatchTime%100, U_TIMS_DispatchDate))[For Dispatch],
		        --DATEADD(HOUR, U_TIMS_ITBinDispTime/100, DATEADD(MINUTE, U_TIMS_ITBinDispTime%100, U_TIMS_ITBinDispDate))[For Dispatch],
		        DATEADD(HOUR, U_TIMS_ITBinILBTime/100, DATEADD(MINUTE, U_TIMS_ITBinILBTime%100, U_TIMS_ITBinILB))[For Irradiation],
		        DATEADD(HOUR, U_TIMS_ITRecBinTime/100, DATEADD(MINUTE, U_TIMS_ITRecBinTime%100, U_TIMS_ITRecBin))[For Storage - Receiving],
		        DATEADD(HOUR, U_TIMS_IrradLoadTime/100, DATEADD(MINUTE, U_TIMS_IrradLoadTime%100, U_TIMS_IrradLoading))[At Irradiation Loading],
		        DATEADD(HOUR, U_TIMS_IUBBinTime/100, DATEADD(MINUTE, U_TIMS_IUBBinTime%100, U_TIMS_IUBBin))[For Storage - Unloading],
		        DATEADD(HOUR, U_TIMS_IUBTime/100, DATEADD(MINUTE, U_TIMS_IUBTime%100, U_TIMS_IrradUnloading))[At Irradiation]
		    FROM OBTN
			WHERE U_SONo = {SONo}
				)
				SELECT 
					'For Storage - Receiving' [Activity]
					,CASE 
						WHEN COUNT(ISNULL([For Storage - Receiving], GETDATE())) = SUM(CASE WHEN [For Storage - Receiving] IS NULL THEN 1 ELSE 0 END) THEN null
						ELSE MIN([For Storage - Receiving])
					END [Start]
					,CASE 
						WHEN SUM(CASE WHEN [For Storage - Receiving] IS NULL THEN 1 ELSE 0 END) > 0 THEN null
						ELSE MAX([For Storage - Receiving])
					END AS [End]
					,CONVERT(VARCHAR, DATEADD(SECOND, DATEDIFF(SECOND, MIN([For Storage - Receiving]), MAX([For Storage - Receiving])), 0), 108) [Duration]
				FROM result

				UNION ALL 

				SELECT 
					'For Irradiation' [Activity]
					,CASE 
						WHEN COUNT(ISNULL(	[For Irradiation], GETDATE())) = SUM(CASE WHEN [For Irradiation] IS NULL THEN 1 ELSE 0 END) THEN null
						ELSE MIN([For Irradiation])
					END [Start]
					,CASE 
						WHEN SUM(CASE WHEN [For Irradiation] IS NULL THEN 1 ELSE 0 END) > 0 THEN null
						ELSE MAX([For Irradiation])
					END AS [End]
					,CONVERT(VARCHAR, DATEADD(SECOND, DATEDIFF(SECOND, MIN([For Irradiation]), MAX([For Irradiation])), 0), 108) [Duration]
				FROM result

				UNION ALL 

				SELECT 
					'At Irradiation Loading' [Activity]
					,CASE 
						WHEN COUNT(ISNULL([At Irradiation Loading], GETDATE())) = SUM(CASE WHEN [At Irradiation Loading] IS NULL THEN 1 ELSE 0 END) THEN null
						ELSE MIN([At Irradiation Loading])
					END [Start]
					,CASE 
						WHEN SUM(CASE WHEN [At Irradiation Loading] IS NULL THEN 1 ELSE 0 END) > 0 THEN null
						ELSE MAX([At Irradiation Loading])
					END AS [End]
					,CONVERT(VARCHAR, DATEADD(SECOND, DATEDIFF(SECOND, MIN([At Irradiation Loading]), MAX([At Irradiation Loading])), 0), 108) [Duration]
				FROM result

				UNION ALL 

				SELECT 
					'At Irradiation' [Activity]
					,CASE 
						WHEN COUNT(ISNULL([At Irradiation], GETDATE())) = SUM(CASE WHEN [At Irradiation] IS NULL THEN 1 ELSE 0 END) THEN null
						ELSE MIN([At Irradiation])
					END [Start]
					,CASE 
						WHEN SUM(CASE WHEN [At Irradiation] IS NULL THEN 1 ELSE 0 END) > 0 THEN null
						ELSE MAX([At Irradiation])
					END AS [End]
					,CONVERT(VARCHAR, DATEADD(SECOND, DATEDIFF(SECOND, MIN([At Irradiation]), MAX([At Irradiation])), 0), 108) [Duration]
				FROM result

				UNION ALL 

				SELECT 
					'For Storage - Unloading' [Activity]
					,CASE 
						WHEN COUNT(ISNULL([For Storage - Unloading], GETDATE())) = SUM(CASE WHEN [For Storage - Unloading] IS NULL THEN 1 ELSE 0 END) THEN null
						ELSE MIN([For Storage - Unloading])
					END [Start]
					,CASE 
						WHEN SUM(CASE WHEN [For Storage - Unloading] IS NULL THEN 1 ELSE 0 END) > 0 THEN null
						ELSE MAX([For Storage - Unloading])
					END AS [End]
					,CONVERT(VARCHAR, DATEADD(SECOND, DATEDIFF(SECOND, MIN([For Storage - Unloading]), MAX([For Storage - Unloading])), 0), 108) [Duration]
				FROM result

				UNION ALL 

				SELECT 
					'For Dispatch' [Activity]
					,CASE 
						WHEN COUNT(ISNULL([For Dispatch], GETDATE())) = SUM(CASE WHEN [For Dispatch] IS NULL THEN 1 ELSE 0 END) THEN null
						ELSE MIN([For Dispatch])
					END [Start]
					,CASE 
						WHEN SUM(CASE WHEN [For Dispatch] IS NULL THEN 1 ELSE 0 END) > 0 THEN null
						ELSE MAX([For Dispatch])
					END AS [End]
					,CONVERT(VARCHAR, DATEADD(SECOND, DATEDIFF(SECOND, MIN([For Dispatch]), MAX([For Dispatch])), 0), 108) [Duration]
				FROM result 
		""";
		var constr = _sql.GetConnection("SAP");
		var result = _sql.GetData<SalesOrderActivity, object>(qry, null, constr, CommandType.Text);

		return result;
	}

	public List<PalletDetails> GetPalletDetailsList(string SONo)
	{
		//string qry = $"""
		//	SELECT 
		//        T0.MnfSerial [PalletNo], 
		//		T1.U_TransferType [Activity],
		//		T1.U_DisplayStatus [Status]
		//	FROM OBTN T0
		//	LEFT JOIN "@SERVICE_DATA_ROW" T1 ON T0.U_TIMS_ITSortCode = T1.U_SortCode 
		//	LEFT JOIN ORDR T2 ON T0.U_SONo = T2.DocEntry AND T1.Code = T2.U_ServiceType
		//	WHERE T0.U_SONo = {SONo}
		//	GROUP BY T0.MnfSerial,T1.U_TransferType,T1.U_DisplayStatus
		//	ORDER BY MnfSerial
		//""";
		string qry = $"""
			SELECT 
		    T0.MnfSerial [PalletNo], 
			CASE WHEN SUM(CASE WHEN T0.U_TIMS_DispatchTime IS NULL THEN 1 ELSE 0 END) > 0 
				THEN  T1.U_TransferType 
				ELSE 'Dispatched'
			END [Activity],
			CASE WHEN SUM(CASE WHEN T0.U_TIMS_DispatchTime IS NULL THEN 1 ELSE 0 END) > 0 
				THEN  T1.U_DisplayStatus 
				ELSE 'Dispatched'
			END [Status]
		FROM OBTN T0
		LEFT JOIN "@SERVICE_DATA_ROW" T1 ON T0.U_TIMS_ITSortCode = T1.U_SortCode 
		LEFT JOIN ORDR T2 ON T0.U_SONo = T2.DocEntry AND T1.Code = T2.U_ServiceType
		WHERE T0.U_SONo = {SONo}
		GROUP BY T0.MnfSerial,T1.U_TransferType,T1.U_DisplayStatus
		ORDER BY MnfSerial
		""";

		var constr = _sql.GetConnection("SAP");
		var result = _sql.GetData<PalletDetails, object>(qry, null, constr, CommandType.Text);

		return result;
	}

	public List<Batch> GetBatches(string SONo)
	{
		string qry = $"""
			SELECT 
				T0.MnfSerial [PalletNo], 
				T0.DistNumber [BatchNo],
				T1.U_TransferType [Activity]
			FROM OBTN T0
			LEFT JOIN "@SERVICE_DATA_ROW" T1 ON T0.U_TIMS_ITSortCode = T1.U_SortCode 
			LEFT JOIN ORDR T2 ON T0.U_SONo = T2.DocEntry AND T1.Code = T2.U_ServiceType
			WHERE T0.U_SONo = {SONo}
		""";
		var constr = _sql.GetConnection("SAP");
		var result = _sql.GetData<Batch, object>(qry, null, constr, CommandType.Text);

		return result;
	}

	// 12/12/2023 - RIP hardcode. 
	Dictionary<string,
		Dictionary<string, string>> udfMapping = new() {
			{ "Irradiation Only", new()
				{
					{ "For Irradiation", nameof(BatchNumberDetail.U_TIMS_ITBinILB) },
					{ "At Irradiation Loading", nameof(BatchNumberDetail.U_TIMS_IrradLoading) },
					{ "At Irradiation", nameof(BatchNumberDetail.U_TIMS_IrradUnloading) },
					{ "For Dispatch", nameof(BatchNumberDetail.U_TIMS_ITBinDispDate) },
				}
			},
			{ "Storage", new()
				{
					{ "For Storage - Receiving", nameof(BatchNumberDetail.U_TIMS_ITRecBin) },
					{ "For Dispatch", nameof(BatchNumberDetail.U_TIMS_ITBinDispDate) },
				}
			},
			{ "Storage + Irradiation + Storage", new()
				{
					{ "For Storage - Receiving", nameof(BatchNumberDetail.U_TIMS_ITRecBin) },
					{ "For Irradiation", nameof(BatchNumberDetail.U_TIMS_ITBinILB) },
					{ "At Irradiation Loading", nameof(BatchNumberDetail.U_TIMS_IrradLoading) },
					{ "At Irradiation", nameof(BatchNumberDetail.U_TIMS_IrradUnloading) },
					{ "For Storage - Unloading", nameof(BatchNumberDetail.U_TIMS_IUBBin) },
					{ "For Dispatch", nameof(BatchNumberDetail.U_TIMS_ITBinDispDate) },
				}
			}
		};

	#endregion

	#region Dashboard
	public async Task<DashboardViewModel> InitializeDashboard()
	{
		DashboardViewModel model = new DashboardViewModel();

		//string qry = $@"Select
		//			A.""DocNum"" as ""SONo""
		//			,A.""CardCode"" as ""CustomerName""
		//			,C.""ItemCode""
		//			,C.""ItemName""
		//			,D.""Name"" as ""ProductType""
		//			,A.""DocStatus""
		//		FROM 
		//			""ORDR"" A
		//		Inner Join 
		//			""RDR1"" B
		//		On 
		//			A.""DocEntry"" = B.""DocEntry""
		//		Inner Join
		//			""OITM"" C
		//		On
		//			B.""ItemCode"" = C.""ItemCode""
		//		Left Join
		//			""@ITEM_SUBGROUP"" D
		//		On
		//			C.""U_ItmSubgroup"" = D.""Code""
		//		Where
		//			A.""DocStatus"" <> 'C'
		//		Order By 
		//			A.""DocNum"" DESC";

		string qry = $@"Select DISTINCT
						A.""DocNum"" as ""SONo""
						,A.""CardCode"" as ""CustomerName""
						,C.""ItemCode""
						,C.""ItemName""
						,D.""Name"" as ""ProductType""
						,A.""DocStatus""
					FROM 
						""ORDR"" A
					Inner Join 
						""RDR1"" B
					On 
						A.""DocEntry"" = B.""DocEntry""
					Inner Join
						""OITM"" C
					On
						B.""ItemCode"" = C.""ItemCode""
					LEFT JOIN OITB TG ON C.""ItmsGrpCod"" = TG.""ItmsGrpCod""
					Left Join
						""@ITEM_SUBGROUP"" D
					On
						C.""U_ItmSubgroup"" = D.""Code""
					Where /* C.ItmsGrpCod = 110 */
					 TG.ItmsGrpNam like '%customer items%'
					and C.InvntItem = 'Y'
					Order By 
						A.""DocNum"" DESC";

		model.SalesOrderList = _sql.GetData<DashboardViewModel.SalesOrders, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

		return model;

	}
	#endregion
}
