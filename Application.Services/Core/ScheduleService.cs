using B1SLayer;

namespace Application.Services.Core;

public class ScheduleService : IScheduleService
{
	private readonly IMsSqlDataAccess _sql;
	private readonly IServiceLayerDataAccess _sl;
	private readonly IQCMaintenanceDataService _maintenanceDataService;
	private readonly IFacilityLocationService _facilityLocationService;
	private readonly IMapper _mapper;
	public ScheduleService(IConfiguration configuration, IServiceLayerDataAccess sl, IQCMaintenanceDataService maintenanceDataService, IFacilityLocationService facilityLocationService, IMapper mapper)
	{
		_sql = new MsSqlDataAccess(configuration);
		_sl = sl;
		_maintenanceDataService = maintenanceDataService;
		_facilityLocationService = facilityLocationService;
		_mapper = mapper;
	}

	public void FilterSchedule(ScheduleViewModel model, string start, string end, string type)
	{
		string filter = string.Empty;
		string field = string.Empty;

		switch (type)
		{
			case "Receiving":
				filter = $@"AND A.""U_PickUpDate"" BETWEEN '{start}' AND '{end}'";
				field = $@"A.""U_PickUpDate""";
				break;
			case "Dispatch":
				filter = $@"AND A.""DocDueDate"" BETWEEN '{start}' AND '{end}'";
				field = $@"A.""DocDueDate""";
				break;
			case "Irradiation":
				filter = $@"AND A.""U_IrridiationDate"" BETWEEN '{start}' AND '{end}'";
				field = $@"A.""U_IrridiationDate""";
				break;
			default: break;
		}

		//string qry = $@"select A.""DocEntry"", A.""DocNum"", CONVERT(VARCHAR,{field},101) as ""DeliveryDate"", A.""U_PaymentSettlement"", A.""CardName"", C.""ItemCode"", C.""ItemName""  
		string qry = $"""
		--select A."DocEntry", A."DocNum", CONVERT(VARCHAR,{field},101) as "DeliveryDate", A."U_PaymentSettlement", A."CardName", B."U_ItemCode" as "ItemCode", C."ItemName", 
		select DISTINCT A."DocEntry", A."DocNum", CONVERT(VARCHAR,{field},101) as "DeliveryDate", A."U_PaymentSettlement", A."CardName", A."U_CustItems" as "ItemCode", C."ItemName", 
			A.U_FacilityLoc[FacilityLocationCode],
			A."U_PONo"[PONo],
			A.U_TIMS_ManufLotNo[MnfLotNo],
			A.U_TIMS_SerOrNo[SrvcOrdrNo]
			from "ORDR" A 
			inner join "RDR1" B ON A."DocEntry" = B."DocEntry" 
			--inner join "OITM" C ON B."U_ItemCode" = C."ItemCode" 
			inner join "OITM" C ON A."U_CustItems" = C."ItemCode" 
			where A."DocStatus" = 'O' 
			AND C."ItmsGrpCod" = 110 
			AND C."InvntItem" = 'Y'
			--and isnull(B.""U_ItemCode"", '') <> ''
			--and A."U_SOStatus" = 'FOR TIMS'
			--and  C."ItmsGrpCod" IN ('110')
			{filter}
			ORDER BY "DeliveryDate" DESC
		""";

		model.ScheduleList = _sql.GetData<Schedules, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);
	}

	public string ConvertToDateTimeFormat(object date, object time)
	{
		string newdate = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
		//string newtime = time.ToString().Contains(":") ? time.ToString() : time.ToString().Substring(0, time.ToString().Length - 2) + time.ToString().Substring(time.ToString().Length - 2, 1) + ":" + time.ToString().Substring(time.ToString().Length - 1); 
		string newtime = time.ToString().PadLeft(4, '0');
		newtime = newtime.Substring(0, 2) + ":" + newtime.Substring(2);
		string newdatetime = $"{newdate} {newtime}";
		return newdatetime;
	}

	#region Receiving Schedule
	public ScheduleViewModel InitializeReceivingSchedule()
	{
		try
		{
			ScheduleViewModel model = new ScheduleViewModel();

			//string qry = $@"select A.""DocEntry"", A.""DocNum"", CONVERT(VARCHAR,A.""DocDueDate"",101) as ""DeliveryDate"", A.""U_PaymentSettlement"", A.""CardName"", C.""ItemCode"" ,C.""ItemName""  
			//string qry = $"""
			//	select 
			//	A."DocEntry", 
			//	A."DocNum", 
			//	A."U_PONo"[PONo],
			//	A.U_TIMS_ManufLotNo[MnfLotNo],
			//	A.U_TIMS_SerOrNo[SrvcOrdrNo],
			//	CONVERT(VARCHAR,A."U_PickUpDate",101) as "DeliveryDate", 
			//	A."U_PaymentSettlement", 
			//	A."CardName", 
			//	B.""U_ItemCode"" as "ItemCode", 
			//	--A."U_CustItems" as "ItemCode", 
			//	C."ItemName"
			//	from "ORDR" A 
			//	inner join "RDR1" B ON A."DocEntry" = B."DocEntry" 
			//	inner join "OITM" C ON B.""U_ItemCode"" = C."ItemCode" 
			//	--inner join "OITM" C ON A."U_CustItems" = C."ItemCode" 
			//	where A."DocStatus" = 'O' 
			//	and isnull(B.""U_ItemCode"", '') <> ''
			//	--and A."U_SOStatus" = 'FOR TIMS'
			//	--AND  C."ItmsGrpCod" IN ('110')
			//	AND A."DocDueDate" = @dateToday
			//	""";
			//AND A.""DocDueDate"" = '2023-04-28'";

			string qry = $@"
				SELECT DISTINCT
					A.""DocEntry"", 
					A.""DocNum"", 
					A.""U_PONo"" AS PONo,
					A.""U_TIMS_ManufLotNo"" AS MnfLotNo,
					A.""U_TIMS_SerOrNo"" AS SrvcOrdrNo,
					CONVERT(VARCHAR, A.""U_PickUpDate"", 101) AS DeliveryDate, 
					A.""U_PaymentSettlement"", 
					A.""CardName"", 
					--B.""U_ItemCode"" AS ItemCode, 
					A.""U_CustItems"" AS ItemCode, 
					C.""ItemName""
				FROM 
					""ORDR"" A 
					INNER JOIN ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry"" 
					--INNER JOIN ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
					INNER JOIN ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
					LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
					
				WHERE 
					A.""DocStatus"" = 'O' 
					--AND ISNULL(B.""U_ItemCode"", '') <> ''
					--AND A.""U_SOStatus"" = 'FOR TIMS'
					--AND C.""ItmsGrpCod"" IN ('110')
					/*AND C.""ItmsGrpCod"" = 110 */			
					AND TG.ItmsGrpNam like '%customer items%'					
					AND C.""InvntItem"" = 'Y'
					AND A.""DocDueDate"" = @dateToday;
			";


			model.ScheduleList = _sql.GetData<Schedules, object>(qry, new { dateToday = DateTime.Today }, _sql.GetConnection("SAP"), CommandType.Text);

			return model;
		}
		catch (Exception)
		{
			throw;
		}

	}
	#endregion

	#region Dispatch Schedule
	public ScheduleViewModel InitializeDispatchSchedule()
	{
		try
		{
			ScheduleViewModel model = new ScheduleViewModel();

			//string qry = $@"select A.""DocEntry"", A.""DocNum"", CONVERT(VARCHAR,A.""U_PickUpDate"",101) as ""DeliveryDate"", A.""U_PaymentSettlement"", A.""CardName"", C.""ItemCode"", C.""ItemName""  
			//string qry = $@"select A.""DocEntry"", A.""DocNum"", CONVERT(VARCHAR,A.""DocDueDate"",101) as ""DeliveryDate"", A.""U_PaymentSettlement"", A.""CardName"", A.""U_CustItems"" as ""ItemCode"", C.""ItemName""
			//string qry = $@"select A.""DocEntry"", A.""DocNum"", CONVERT(VARCHAR,A.""DocDueDate"",101) as ""DeliveryDate"", A.""U_PaymentSettlement"", A.""CardName"", B.""U_ItemCode"" as ""ItemCode"", C.""ItemName""
			//         from ""ORDR"" A 
			//         inner join ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry"" 
			//         inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
			//         --inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
			//         where A.""DocStatus"" = 'O' 
			//and isnull(B.""U_ItemCode"", '') <> ''
			//--and A.""U_SOStatus"" = 'FOR TIMS'
			//         --AND  C.""ItmsGrpCod"" IN ('110')
			//AND A.""U_PickUpDate"" = '{DateTime.Today.ToString("yyyy-MM-dd")}'";
			//AND A.""DocDueDate"" = '2023-04-28'";

			string qry = $@"
				SELECT DISTINCT
					A.""DocEntry"",
					A.""DocNum"",
					CONVERT(VARCHAR, A.""DocDueDate"", 101) AS ""DeliveryDate"",
					A.""U_PaymentSettlement"",
					A.""CardName"",
					A.""U_CustItems"" AS ""ItemCode"",
					--B.""U_ItemCode"" AS ""ItemCode"",
					C.""ItemName""
				FROM 
					""ORDR"" A 
					INNER JOIN ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry"" 
					--INNER JOIN ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
					INNER JOIN ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
				WHERE 
					A.""DocStatus"" = 'O' 
					--AND ISNULL(B.""U_ItemCode"", '') <> ''
					--AND A.""U_SOStatus"" = 'FOR TIMS'
					--AND C.""ItmsGrpCod"" IN ('110')
					AND C.""ItmsGrpCod"" = 110 
					AND C.""InvntItem"" = 'Y'
					AND A.""U_PickUpDate"" = '{DateTime.Today.ToString("yyyy-MM-dd")}'
			";

			model.ScheduleList = _sql.GetData<Schedules, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			return model;
		}
		catch (Exception)
		{

			throw;
		}
	}
	#endregion

	#region Irradiation Schedule
	public ScheduleViewModel InitializeIrradiationSchedule()
	{
		try
		{
			ScheduleViewModel model = new ScheduleViewModel();

			//string qry = $@"select A.""DocEntry""
			//		, A.""DocNum""
			//		, CONVERT(VARCHAR,A.""U_IrridiationDate"",101) as ""DeliveryDate""
			//		, A.""U_PaymentSettlement""
			//		, A.""CardCode""
			//		, A.""CardName""
			//		, B.""U_ItemCode"" as ""ItemCode""
			//		--, A.""U_CustItems"" as ""ItemCode""
			//		, C.""ItemName""
			//		, A.U_FacilityLoc[FacilityLocationCode]
			//         from ""ORDR"" A 
			//         inner join ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry"" 
			//         inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
			//         --inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
			//         where A.""DocStatus"" = 'O' 
			//and isnull(B.""U_ItemCode"", '') <> ''
			//--and A.""U_SOStatus"" = 'FOR TIMS'
			//         --AND  C.""ItmsGrpCod"" IN ('110')
			//AND A.""U_IrridiationDate"" = '{DateTime.Today.ToString("yyyy-MM-dd")}'";
			//AND A.""U_IrridiationDate"" = '05-01-2023'";

			string qry = $@"
					SELECT DISTINCT
						A.""DocEntry"",
						A.""DocNum"",
						CONVERT(VARCHAR, A.""U_IrridiationDate"", 101) AS ""DeliveryDate"",
						A.""U_PaymentSettlement"",
						A.""CardCode"",
						A.""CardName"",
						--B.""U_ItemCode"" AS ""ItemCode"",
						A.""U_CustItems"" AS ""ItemCode"",
						C.""ItemName"",
						A.U_FacilityLoc AS FacilityLocationCode
					FROM 
						""ORDR"" A 
						INNER JOIN ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry"" 
						--INNER JOIN ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
						INNER JOIN ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
					WHERE 
						A.""DocStatus"" = 'O' 
						--AND ISNULL(B.""U_ItemCode"", '') <> ''
						--AND A.""U_SOStatus"" = 'FOR TIMS'
						--AND C.""ItmsGrpCod"" IN ('110')
						AND C.""ItmsGrpCod"" = 110 
						AND C.""InvntItem"" = 'Y'
						AND A.""U_IrridiationDate"" = '{DateTime.Today.ToString("yyyy-MM-dd")}'
				";

			model.ScheduleList = _sql.GetData<Schedules, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			return model;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public ScheduleViewModel getAllSchedules(string location)
	{
		ScheduleViewModel model = new ScheduleViewModel();

		model.events = new List<ScheduleEvents>();
		ScheduleEvents scheduleEvents = new ScheduleEvents();
		scheduleEvents.extendedProps = new extendedProps();

		//        id: 1,
		//        url: '',
		//        title: 'Design Review',
		//        start: date,
		//        end: nextDay,
		//        allDay: false,
		//        extendedProps: {
		//            calendar: 'Scheduled'
		//        }

		string qry = $"""
			WITH ReceivedSalesOrders AS (
				SELECT DISTINCT(U_SONo)[DocEntry] FROM OBTN WHERE NULLIF(U_SONo, '') IS NOT NULL 
			)

			SELECT DISTINCT
				A."DocEntry",
				A."DocNum",
				A."CardName",
				A."U_PickUpDate",
				A."U_IrridiationDate",
				A."U_IrridiationStart",
				A."U_IrridiationEnd",
				A."U_PaymentSettlement",
				A."U_Remarks",
				C."ItemName",
				--B.""U_ItemCode"" AS "ItemCode",
				A."U_CustItems" AS "ItemCode",
				A.U_FacilityLoc,
				CAST(CASE WHEN D.DocEntry IS NOT NULL THEN 1 ELSE 0 END AS BIT)[IsReceived]
			FROM "ORDR" A
				inner join "RDR1" B on A."DocEntry" = B."DocEntry"
				--INNER JOIN "OITM" C ON B.""U_ItemCode"" = C."ItemCode"
				INNER JOIN "OITM" C ON A."U_CustItems" = C."ItemCode"
				LEFT JOIN ReceivedSalesOrders D ON A.DocEntry = D.DocEntry
			where 
				A."DocStatus" = 'O'
				AND ISNULL(A."U_IrridiationDate", '') <> ''
				AND ISNULL(A."U_IrridiationStart", '') <> ''
				AND ISNULL(A."U_IrridiationEnd", '') <> ''
				AND C."ItmsGrpCod" = 110 
				AND C."InvntItem" = 'Y'
				--and isnull(B.""U_ItemCode"", '') <> ''
			""";
		// 
		var queryResult = _sql.GetData(qry, _sql.GetConnection("SAP")).AsEnumerable().ToList();

		//1. get all received data via ["IsReceived"] from queryResult
		//2. create new list  of objects that's the same structure as line 212 and put "Receivied" in extendedProps property
		var receivedObjectSched = queryResult.Where(x => (bool)x["IsReceived"]).Select(
			x => new
			{
				id = Convert.ToInt32(x["DocEntry"]),
				url = "",
				title = $"{x["CardName"].ToString()}-{x["DocNum"].ToString()}",
				start = ConvertToDateTimeFormat(x["U_PickUpDate"], 0),
				end = ConvertToDateTimeFormat(x["U_PickUpDate"], 0),
				allDay = false,
				extendedProps = new
				{
					calendar = "Received",
					start = ConvertToDateTimeFormat(x["U_IrridiationDate"], x["U_IrridiationStart"]),
					end = ConvertToDateTimeFormat(x["U_IrridiationDate"], x["U_IrridiationEnd"])
				},
				description = x["U_Remarks"].ToString(),
				itemname = x["ItemName"].ToString(),
				noofdosimeters = _maintenanceDataService.GetQCMaintenanceByItem(x["ItemCode"]?.ToString() ?? "")?.NumberOfDosimeters ?? "0"
				//facilityLocation = (string)x["FacilityLocName"]
			}).ToList();

		var objectsched = queryResult.Select(
			x => new
			{
				id = Convert.ToInt32(x["DocEntry"]),
				url = "",
				title = $"{x["CardName"].ToString()}-{x["DocNum"].ToString()}",
				start = ConvertToDateTimeFormat(x["U_IrridiationDate"], x["U_IrridiationStart"]),
				end = ConvertToDateTimeFormat(x["U_IrridiationDate"], x["U_IrridiationEnd"]),
				allDay = false,
				extendedProps = new
				{
					calendar = x["U_PaymentSettlement"].ToString(),
					start = ConvertToDateTimeFormat(x["U_IrridiationDate"], x["U_IrridiationStart"]),
					end = ConvertToDateTimeFormat(x["U_IrridiationDate"], x["U_IrridiationEnd"])
				}, //3. use U_PaymentSettlement only, remove is received stuff
				description = x["U_Remarks"].ToString(),
				itemname = x["ItemName"].ToString(),
				noofdosimeters = _maintenanceDataService.GetQCMaintenanceByItem(x["ItemCode"]?.ToString() ?? "")?.NumberOfDosimeters ?? "0"
				//facilityLocation = (string)x["FacilityLocName"]
			}).ToList();

		//4. add the received list (from step 2) and put it in objectsched variable
		//receivedObjectSched.CopyTo(objectsched.ToArray());
		objectsched.AddRange(receivedObjectSched);

		foreach (var sched in objectsched)
		{
			scheduleEvents = new ScheduleEvents();
			scheduleEvents.extendedProps = new extendedProps();

			scheduleEvents.id = sched.id;
			scheduleEvents.url = sched.url;
			scheduleEvents.title = sched.title;
			scheduleEvents.start = sched.start;
			scheduleEvents.end = sched.end;
			scheduleEvents.allDay = sched.allDay;
			scheduleEvents.extendedProps.calendar = sched.extendedProps.calendar;
			scheduleEvents.extendedProps.start = sched.extendedProps.start;
			scheduleEvents.extendedProps.end = sched.extendedProps.end;
			scheduleEvents.extendedProps.description = sched.description;
			scheduleEvents.extendedProps.itemname = sched.itemname;
			scheduleEvents.extendedProps.noofdosimeters = sched.noofdosimeters;
			model.events.Add(scheduleEvents);
		}

		var scheduledDates = GetScheduledDates(location);
		model.availableHours = scheduledDates;

		return model;
	}

	private Dictionary<string, double> GetScheduledDates(string location)
	{
		string query = @"
			WITH IrradDoc as (
			SELECT DISTINCT
				T0.DocEntry,
				T0.DocNum,
				T0.U_FacilityLoc, 
				T1.U_Duration,
				T0.U_IrridiationDate,
				T0.U_IrridiationStart,
				T0.U_IrridiationEnd,
				(DATEDIFF(SECOND, X0.IrradStartDateTime, X0.IrradEndDateTime) / 3600.0 )[IrradSpanHours],
				X0.*
				FROM ORDR T0
				OUTER APPLY 
				(
					SELECT 
					DATETIMEFROMPARTS(
						YEAR(T0.U_IrridiationDate), 
						MONTH(T0.U_IrridiationDate), 
						DAY(T0.U_IrridiationDate),
						(U_IrridiationStart / 100),
						(U_IrridiationStart % 100),
						0,
						0
					)[IrradStartDateTime],
					DATETIMEFROMPARTS(
						YEAR(T0.U_IrridiationDate), 
						MONTH(T0.U_IrridiationDate), 
						DAY(T0.U_IrridiationDate),
						(U_IrridiationEnd / 100),
						(U_IrridiationEnd % 100),
						0,
						0
					)[IrradEndDateTime]
					FROM ORDR I0
					WHERE T0.DocEntry = I0.DocEntry
				) AS X0
				JOIN ""@FACILITYLOCATION"" T1 ON T1.Code = T0.U_FacilityLoc
				WHERE
				U_IrridiationDate IS NOT NULL AND
				U_IrridiationStart IS NOT NULL AND
				U_IrridiationEnd IS NOT NULL
			),
			FacultyLocs as (
				SELECT U_FacilityLoc, U_IrridiationDate, SUM(IrradSpanHours)[AllocHours]
				FROM IrradDoc GROUP BY U_FacilityLoc, U_IrridiationDate
			)

			SELECT *, (T1.U_Duration - T0.AllocHours)[AvailableHours] FROM FacultyLocs T0
			JOIN ""@FACILITYLOCATION"" T1 ON T1.Code = T0.U_FacilityLoc
			WHERE T1.Code = @location;

			--SELECT * FROM IrradDoc;
		";

		string constr = _sql.GetConnection("SAP");
		var data = _sql.GetData<dynamic, object>(query, new { location }, constr, CommandType.Text);

		var res = data.Select(x => new
		{
			U_IrridiationDate = ((DateTime)x.U_IrridiationDate).ToString("yyyy-MM-dd"),
			x.AvailableHours
		}).ToDictionary(x => (string)x.U_IrridiationDate, x => (double)x.AvailableHours);

		return res;
	}
	public bool updateEventSchedule(ScheduleEvents eventData)
	{
		try
		{
			ScheduleHeader scheduleHeader = new ScheduleHeader();
			scheduleHeader.U_PaymentSettlement = eventData.extendedProps.calendar;
			scheduleHeader.U_Remarks = eventData.extendedProps.description;
			scheduleHeader.U_IrridiationDate = Convert.ToDateTime(eventData.start).ToString("yyyy-MM-dd");
			scheduleHeader.U_IrridiationStart = Convert.ToDateTime(eventData.start).ToString("HH:mm:ss");
			scheduleHeader.U_IrridiationEnd = Convert.ToDateTime(eventData.end).ToString("HH:mm:ss");

			int id = Convert.ToInt32(eventData.id);

			string jsonString = JsonConvert.SerializeObject(scheduleHeader);

			_sl.PatchAsync<dynamic>("Orders", id, scheduleHeader);

			return true;
		}
		catch (Exception ex)
		{

			throw ex;
		}
	}

	public async Task<bool> updateEventsSchedule(List<ScheduleEvents> eventsData)
	{
		try
		{
			List<SLBatchRequest> batchReqs = new();
			foreach (ScheduleEvents e in eventsData)
			{
				ScheduleHeader scheduleHeader = new ScheduleHeader();
				scheduleHeader.U_PaymentSettlement = e.extendedProps.calendar;
				scheduleHeader.U_Remarks = e.extendedProps.description;
				scheduleHeader.U_IrridiationDate = Convert.ToDateTime(e.start).ToString("yyyy-MM-dd");
				scheduleHeader.U_IrridiationStart = Convert.ToDateTime(e.start).ToString("HH:mm:ss");
				scheduleHeader.U_IrridiationEnd = Convert.ToDateTime(e.end).ToString("HH:mm:ss");

				batchReqs.Add(
					new SLBatchRequest(
						HttpMethod.Patch,
						$"Orders({e.id})",
						scheduleHeader,
						contentID: batchReqs.Count + 1
					)
				);
			}

			var result = await _sl.BatchAsync(batchReqs.ToArray());
			var errors = result.Where(x => !x.IsSuccessStatusCode);
			if (errors.Any())
			{
				var resp = errors.First();
				var error = await resp.Content.ReadAsStringAsync();
				resp.EnsureSuccessStatusCode();
			}
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
	#endregion

	#region Schedule Details
	public ScheduleViewModel InitializeScheduleDetails(string DocEntry, string ItemCode)
	{
		try
		{
			ScheduleViewModel model = new ScheduleViewModel();

			//string qry = $@"select A.""DocEntry""
			//					, A.""DocNum""
			//					, A.""CardName""
			//					, A.""DocStatus""
			//					, A.""U_IrridiationDate""
			//					, A.""U_PickUpDate""
			//					, A.""DocDueDate"" as ""DeliveryDate""
			//					, A.""U_PaymentSettlement""
			//					, A.""U_Remarks"" as ""Remarks""
			//					,A.""U_PONo""[PONo]
			//					,A.U_TIMS_ManufLotNo[MnfLotNo]
			//					,A.U_TIMS_SerOrNo[SrvcOrdrNo]
			//					,CASE 
			//						WHEN LEN(A.""U_IrridiationStart"") <= 4 
			//							THEN
			//							DATEADD(SECOND, FLOOR((A.""U_IrridiationStart""/100)*60)*60 + (A.""U_IrridiationStart""%100)* 60, A.""CreateDate"") 
			//						WHEN LEN(A.""U_IrridiationStart"") > 4 
			//							THEN 
			//							DATEADD(SECOND,FLOOR(A.""U_IrridiationStart""/1000)*3600 + 
			//							FLOOR(((A.""U_IrridiationStart""%1000)/100)*60)*60 + (A.""U_IrridiationStart""%100) * 60, A.""CreateDate"") 
			//					END 
			//					AS ""U_IrridiationStart""
			//					,CASE 
			//						WHEN LEN(A.""U_IrridiationEnd"") <= 4 
			//							THEN
			//							DATEADD(SECOND, FLOOR((A.""U_IrridiationEnd""/100)*60)*60 + (A.""U_IrridiationEnd""%100)* 60, A.""CreateDate"") 
			//						WHEN LEN(A.""U_IrridiationStart"") > 4 
			//							THEN 
			//							DATEADD(SECOND,FLOOR(A.""U_IrridiationStart""/1000)*3600 + 
			//							FLOOR(((A.""U_IrridiationEnd""%1000)/100)*60)*60 + (A.""U_IrridiationEnd""%100) * 60, A.""CreateDate"") 
			//					END 
			//					AS ""U_IrridiationEnd""
			//					, B.""U_ItemCode"" as ""ItemCode""
			//					--, A.""U_CustItems"" as ""ItemCode""
			//					, C.""ItemName""
			//					, C.""U_NoBoxesPallet"" as ""NoOfBoxesPerPallet""
			//					, F.""Quantity"" as ""NoOfBoxes""
			//					, CEILING(CAST(F.""Quantity"" AS DECIMAL(10, 2)) / CAST(C.""U_NoBoxesPallet"" AS DECIMAL(10, 2))) AS ""NoOfPallets""
			//					--, CEILING(F.""Quantity"" / C.""U_NoBoxesPallet"") AS ""NoOfPallets""
			//					, A.""U_FacilityLoc""[FacilityLocationCode]
			//					from ""ORDR"" A 
			//					inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
			//					inner join ""OITM"" C on B.""U_ItemCode"" = C.""ItemCode""
			//					--inner join ""OITM"" C on A.""U_CustItems"" = C.""ItemCode""
			//					outer apply (select CAST(ROUND(D.""Quantity"", 0) AS INT) as ""Quantity""
			//							from ""RDR1"" D 
			//							inner join ""OITM"" E on D.""ItemCode"" = E.""ItemCode""
			//							where D.""DocEntry"" = {DocEntry} ) F
			//							--and E.""ItmsGrpCod"" = '101'
			//					where A.""DocEntry"" = {DocEntry}
			//					and B.""U_ItemCode"" = '{ItemCode}'";
			//and A.""U_CustItems"" = '{ItemCode}'";

			string qry = $@"
				SELECT DISTINCT
					A.""DocEntry"",
					A.""DocNum"",
					A.""CardName"",
					A.""DocStatus"",
					A.""U_IrridiationDate"",
					A.""U_PickUpDate"",
					A.""DocDueDate"" AS ""DeliveryDate"",
					A.""U_PaymentSettlement"",
					A.""U_Remarks"" AS ""Remarks"",
					A.""U_PONo"" AS PONo,
					A.U_TIMS_ManufLotNo AS MnfLotNo,
					A.U_TIMS_SerOrNo AS SrvcOrdrNo,
					CASE 
						WHEN LEN(A.""U_IrridiationStart"") <= 4 
							THEN
							DATEADD(SECOND, FLOOR((A.""U_IrridiationStart"" / 100) * 60) * 60 + (A.""U_IrridiationStart"" % 100) * 60, A.""CreateDate"") 
						WHEN LEN(A.""U_IrridiationStart"") > 4 
							THEN 
							DATEADD(SECOND, FLOOR(A.""U_IrridiationStart"" / 1000) * 3600 + 
							FLOOR(((A.""U_IrridiationStart"" % 1000) / 100) * 60) * 60 + (A.""U_IrridiationStart"" % 100) * 60, A.""CreateDate"") 
					END AS ""U_IrridiationStart"",
					CASE 
						WHEN LEN(A.""U_IrridiationEnd"") <= 4 
							THEN
							DATEADD(SECOND, FLOOR((A.""U_IrridiationEnd"" / 100) * 60) * 60 + (A.""U_IrridiationEnd"" % 100) * 60, A.""CreateDate"") 
						WHEN LEN(A.""U_IrridiationStart"") > 4 
							THEN 
							DATEADD(SECOND, FLOOR(A.""U_IrridiationStart"" / 1000) * 3600 + 
							FLOOR(((A.""U_IrridiationEnd"" % 1000) / 100) * 60) * 60 + (A.""U_IrridiationEnd"" % 100) * 60, A.""CreateDate"") 
					END AS ""U_IrridiationEnd"",
					--B.""U_ItemCode"" AS ""ItemCode"",
					A.""U_CustItems"" AS ""ItemCode"",
					C.""ItemName"",
					C.""U_NoBoxesPallet"" AS ""NoOfBoxesPerPallet"",
					F.""Quantity"" AS ""NoOfBoxes"",
					CEILING(CAST(F.""Quantity"" AS DECIMAL(10, 2)) / CAST(C.""U_NoBoxesPallet"" AS DECIMAL(10, 2))) AS ""NoOfPallets"",
					A.""U_FacilityLoc"" AS FacilityLocationCode
				FROM 
					""ORDR"" A 
					INNER JOIN ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry""
					--INNER JOIN ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode""
					INNER JOIN ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode""
					LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
					OUTER APPLY (
						SELECT CAST(ROUND(D.""Quantity"", 0) AS INT) AS ""Quantity""
						FROM ""RDR1"" D 
						INNER JOIN ""OITM"" E ON D.""ItemCode"" = E.""ItemCode""
						LEFT JOIN OITB xTG ON E.ItmsGrpCod = xTG.ItmsGrpCod
						WHERE D.""DocEntry"" = {DocEntry}
						/*AND E.""ItmsGrpCod"" = 110 */
						AND xTG.ItmsGrpNam like '%customer items%'
						AND E.""InvntItem"" = 'Y'
					) F
				WHERE 
					A.""DocEntry"" = {DocEntry}
					--AND B.""U_ItemCode"" = '{ItemCode}'
					AND A.""U_CustItems"" = '{ItemCode}'
					/*AND C.""ItmsGrpCod"" = 110 */
					AND TG.ItmsGrpNam like '%customer items%'
					AND C.""InvntItem"" = 'Y'
			";


			//string qry = $@"select A.""DocEntry"", A.""DocNum"", A.""CardName"", A.""DocStatus"", A.""U_IrridiationDate"", A.""U_PickUpDate"", A.""DocDueDate"" as ""DeliveryDate""
			//			, A.""U_IrridiationStart""
			//			, A.""U_IrridiationEnd""
			//			from ""ORDR"" A 
			//			where A.""DocEntry"" = {DocEntry}
			//			";
			model.ScheduleDetails = _sql.FirstOrDefault<Schedules, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);
			if (model.ScheduleDetails.FacilityLocationCode is string id)
			{
				var res = _facilityLocationService.Get(id);
				var mapped = _mapper.Map<FacilityLocation>(res);
				model.ScheduleDetails.FacilityLocation = mapped;
			}

			model.ScheduleDetails.Duration = model.ScheduleDetails.U_IrridiationEnd - model.ScheduleDetails.U_IrridiationStart;

			qry = $@"select DISTINCT A.""U_PaymentSettlement"" as ""Status""
						, B.""MnfSerial"" as ""PalletNumber""
						, B.""DistNumber"" as ""BoxBatch"" --Changed ""BoxBatch"" to DistNumber only -Karl 11/29/2023
						--, CONCAT(A.""DocEntry"" , '-' , B.""DistNumber"") as ""BoxBatch""
					from ""ORDR"" A 
					inner join ""RDR1"" C on A.""DocEntry"" = C.""DocEntry""
					--inner join ""OBTN"" B on B.""ItemCode"" = C.""U_ItemCode""
					inner join ""OBTN"" B on B.""ItemCode"" = A.""U_CustItems""
					where A.""DocEntry"" = {DocEntry}
					and B.""U_SONo"" = {DocEntry} --Added U_SONo to filtering -Karl 11/29/2023
					--and C.""U_ItemCode"" = '{ItemCode}'
					and A.""U_CustItems"" = '{ItemCode}'
					";

			model.ScheduleDetails.NoOfDosimeters = _maintenanceDataService.GetQCMaintenanceByItem(model.ScheduleDetails.ItemCode)?.NumberOfDosimeters ?? "0";

			model.ItemList = _sql.GetData<Items, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);



			return model;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<bool> updateScheduleDetails(string id, string DispatchDate, string IrradDate, string IrradiationStart, string IrradiationEnd, string Remarks)
	{
		try
		{
			string jsonString = $@"{{ ""U_PickUpDate"" : ""{DispatchDate}"", ""U_IrridiationDate"" : ""{IrradDate}"", ""U_IrridiationStart"" : ""{IrradiationStart}"", ""U_IrridiationEnd"" : ""{IrradiationEnd}"", ""U_Remarks"" : ""{Remarks}"" }}";

			await _sl.PatchStringAsync("Orders", Convert.ToInt32(id), jsonString);

			return true;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
	#endregion
}
