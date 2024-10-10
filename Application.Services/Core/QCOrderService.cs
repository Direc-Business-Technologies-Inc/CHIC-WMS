using static Application.Models.ViewModels.FormsAndReportsViewModel.IrradiationSalesOrderDetailsViewModel;

namespace Application.Services.Core;

public class QCOrderService : IQCOrderService
{
	private readonly IMsSqlDataAccess _sql;
	private readonly IServiceLayerDataAccess _sl;
	private readonly IQCMaintenanceDataService _dataQCMaintenance;
	private readonly IQCOrderDataService _dataQCOrder;
	private readonly ICertificatOfIrradiationDataService _certificateOfIrradiationDataService;
	private readonly IMapper Mapper;

	public QCOrderService(IConfiguration configuration, IMapper mapper, IQCMaintenanceDataService dataQCMaintenance, IQCOrderDataService qCOrder, ICertificatOfIrradiationDataService certificateOfIrradiationDataService, IServiceLayerDataAccess sl)
	{
		_sql = new MsSqlDataAccess(configuration);
		Mapper = mapper;
		_dataQCMaintenance = dataQCMaintenance;
		_dataQCOrder = qCOrder;
		_certificateOfIrradiationDataService = certificateOfIrradiationDataService;
		_sl = sl;
	}

	public QCOrderViewModel InitializeQCOrder()
	{
		QCOrderViewModel model = new QCOrderViewModel();

		List<DataManager.Models.QCMaintenance.InspectionPlan> inspectionPlans = _dataQCMaintenance.GetQCMaintenance();

		model.InspectionPlanList = Mapper.Map<List<QCOrderViewModel.InspectionPlan>>(inspectionPlans);

		var ItemCodeList = model.InspectionPlanList.DistinctBy(x => x.ItemCode).Select(x => $"'{x.ItemCode}'").ToList();

		string ItemCode = string.Join(", ", ItemCodeList);

		//string qry = $@"select ""UgpCode"" as ""Code"", ""UgpName"" as ""Name"" from OUGP";
		//model.UoMList = _sql.GetData<QCOrderViewModel.UoM, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

		//QCOrderViewModel.UoM item = new QCOrderViewModel.UoM();
		//item.Code = "N/A";
		//item.Name = "N/A";

		//model.UoMList.Insert(0, item);

		string qry = $@"select 
					DISTINCT
					A.""DocEntry"" as ""SONo""
					, A.""DocNum""
					, CONVERT(VARCHAR,A.""DocDueDate"",101) as ""ReceivingDate""
					, A.""U_PaymentSettlement""
					, A.""CardName"" as ""CustomerName""
					, A.""CardCode"" as ""CustomerCode""
					--, B.""U_ItemCode""  as ""ItemCode""
					, A.""U_CustItems""  as ""ItemCode""
					, C.""ItemName""
					, F.""Quantity"" as ""NoOfBoxes""
					, D.""UgpName"" as ""UoM""
					, C.""U_TIMS_StorCon"" as ""StorageConditions""
					, A.""U_SOStatus""
            from ""ORDR"" A 
			inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
            inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
			
            --inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
			left join ""OUGP"" D ON C.""UgpEntry"" = D.""UgpEntry""
			outer apply (select TOP 1 CAST(ROUND(D.""Quantity"", 0) AS INT) as ""Quantity""
													from ""RDR1"" D 
													--inner join ""OITM"" E on D.""U_ItemCode"" = E.""ItemCode""
													inner join ""OITM"" E on D.""ItemCode"" = E.""ItemCode""
													LEFT JOIN OITB xTG ON E.ItmsGrpCod = xTG.ItmsGrpCod
													where D.""DocEntry"" = A.""DocEntry"" 
													/*AND E.""ItmsGrpCod"" = 110 */
													AND xTG.ItmsGrpNam like '%customer items%'
													AND E.""InvntItem"" = 'Y'
													--and isnull(D.""U_ItemCode"", '') <> ''
													) F
			--outer apply (select DATEADD(SECOND, FLOOR((A.""U_IrridiationEnd""/100)*60)*60 + (A.""U_IrridiationEnd""%100)* 60, A.""U_IrridiationDate"") as ""IrradiationDate"") G
            where A.""DocStatus"" = 'O' 
			and CONVERT(INT, F.""Quantity"") > 0
			--and A.U_SOStatus = 'Irradiated - In Storage - For QA'
			--and G.""IrradiationDate"" < GETDATE()
			--and B.""U_ItemCode"" IN({(ItemCode == "" ? "''" : ItemCode)})
			and A.""U_CustItems"" IN({(ItemCode == "" ? "''" : ItemCode)})
			/*AND C.""ItmsGrpCod"" = 110 */
			AND TG.ItmsGrpNam like '%customer items%'
			AND C.""InvntItem"" = 'Y'
			--and isnull(B.""U_ItemCode"", '') <> ''
			order by A.""DocNum"" desc
			";

		model.SalesOrderList = _sql.GetData<QCOrderViewModel.SalesOrderDetail, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

		var qcorderList = _dataQCOrder.GetQCOrderList();

		model.QCOrderList = Mapper.Map<List<QCOrders>>(qcorderList);

		model.DosimetryList = _sql.GetData<QCOrderViewModel.DosimetryType, dynamic>("SELECT * FROM \"@DOSIMETER_TYPE\" ", new { }, _sql.GetConnection("SAP"), CommandType.Text);
		return model;
	}

	public List<QCOrderViewModel.ParameterDetail> GetParameters(string InspectionPlanCode, string Version)
	{
		var ParameterList = _dataQCMaintenance.GetInspectionPlanParameters(InspectionPlanCode, Version);
		return Mapper.Map<List<QCOrderViewModel.ParameterDetail>>(ParameterList);
	}

	public async Task<(bool, string)> SaveQCOrder(QCOrderViewModel.QCOrderDetail model)
	{
		try
		{
			QCOrder qcOrder = Mapper.Map<QCOrder>(model);

			//_dataQCOrder.PostQCOrder(qcOrder);
			return (true, _dataQCOrder.PostQCOrder(qcOrder).QCOrderNo);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<bool> PatchQCOrder(QCOrderViewModel.QCOrderDetail model)
	{
		try
		{
			QCOrder qcOrder = Mapper.Map<QCOrder>(model);

			_dataQCOrder.PatchQCOrder(qcOrder);
			return true;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<QCOrderDetail> SelectQCOrder(string QCOrderNo)
	{
		try
		{
			QCOrder qcOrder = _dataQCOrder.GetQCOrder(QCOrderNo);

			return Mapper.Map<QCOrderDetail>(qcOrder);

		}
		catch (Exception)
		{
			throw;
		}
	}
	public async Task<QCOrderDetail> SelectQCOrder(string ItemCode, string PlanType)
	{
		try
		{
			QCOrder qcOrder = _dataQCOrder.GetQCOrder(ItemCode, PlanType);

			if (qcOrder != null)
			{
				return Mapper.Map<QCOrderDetail>(qcOrder);
			}

			return null;

		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<CertificateOfIrradiation> PostCOI(string Method, QCOrderDetail model)
	{
		try
		{
			//Bakit bigla ka nalang di gumana :((( *suntok sa pader*
			//var COI = Mapper.Map<CertificateOfIrradiation>(model);

			//for manufacturing lotno and po no
			string qry = $@"select U_TIMS_ManufLotNo, U_PONo from ORDR where DocNum = {model.DocNo}";

			var res = _sql.GetData<dynamic, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text).FirstOrDefault();

			CertificateOfIrradiation COI = new CertificateOfIrradiation();
			COI.QCOrderNo = model.QCOrderNo;
			COI.DocNo = model.DocNo;
			COI.CustomerCode = model.CustomerCode;
			COI.CustomerName = model.CustomerName;
			COI.ItemCode = model.ItemCode;
			COI.ItemName = model.ItemName;
			COI.ManufacturingLotNo = res?.U_TIMS_ManufLotNo ?? "";
			COI.CustomerPONo = res?.U_PONo ?? "";
			COI.QCRemarks = model.Remarks;

			(COI.IrradiationDate, COI.TotalNoOfBoxes, COI.FacilityName) = GetCOIAdditionalInfo(COI.DocNo.ToString());

			var parameter = model.SampleDetails.SampleDetailList.FirstOrDefault().ParameterDetailList.Where(x => x.Parameter.ToLower() == "dosimetry" || x.Parameter.ToLower() == "dosimeter" || x.Parameter.ToLower() == "dose").FirstOrDefault();

			COI.MinValue = parameter != null ? parameter.MinValue : "";
			COI.MaxValue = parameter != null ? parameter.MaxValue : "";

			double minActualValue = double.MaxValue;
			double maxActualValue = double.MinValue;

			foreach (var sample in model.SampleDetails.SampleDetailList)
			{
				double val = Convert.ToDouble(sample.ParameterDetailList.Where(x => x.Parameter.ToLower() == "dosimetry" || x.Parameter.ToLower() == "dosimeter" || x.Parameter.ToLower() == "dose").FirstOrDefault().ActualValue);
				maxActualValue = maxActualValue > val ? maxActualValue : val;
				minActualValue = minActualValue < val ? minActualValue : val;
			}

			string actualValueString = maxActualValue == minActualValue ? "" : $" to {maxActualValue}";
			COI.ActualValue = $"{minActualValue}{actualValueString} kGy";

			COI.Layout = "CertificateOfIrradiation.Rpt";//HARDCODED. TO BE ASSIGNED LATER ON
														//COI.FacilityName = "ISI Tanay Ebeam Irradiation Facility"; //HARDCODED. TO BE ASSIGNED LATER ON
			COI.QCRequester = "Admin"; //HARDCODED. TO BE ASSIGNED TO CURRENT USER LATER ON

			COI.Status = "For Approval";

			//LEAVE BLANK
			COI.ApproverJobTitle = "";
			COI.ApproverName = "";
			COI.ApproverRemarks = "";
			COI.DosimetryFilm = model.DosimetryUsed;
			return _certificateOfIrradiationDataService.Post(COI);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public (DateTime, int, string) GetCOIAdditionalInfo(string DocNo)
	{
		DateTime IrradDate = DateTime.Now;
		int NoOfBoxes = 0;
		string FacilityLoc = "";

		string qry = $@"SELECT 
							F.""Quantity"" as ""NoOfBoxes""
							, A.""U_IrridiationDate"" as ""IrradDate""
							, A.""U_FacilityLoc"" as ""FacilityLoc""
						from ""ORDR"" A 
						inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
						--inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
						inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
						LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
						left join ""OUGP"" D ON C.""UgpEntry"" = D.""UgpEntry""
						outer apply (select CAST(ROUND(D.""Quantity"", 0) AS INT) as ""Quantity""
										from ""RDR1"" D 
										--inner join ""OITM"" E on D.""U_ItemCode"" = E.""ItemCode""
										inner join ""OITM"" E on D.""ItemCode"" = E.""ItemCode""
										LEFT JOIN OITB xTG ON E.ItmsGrpCod = xTG.ItmsGrpCod
										where D.""DocEntry"" = A.""DocEntry""
										/*AND E.""ItmsGrpCod"" = 110 */
										AND xTG.ItmsGrpNam like '%customer items%'
										AND E.""InvntItem"" = 'Y'
										--and isnull(D.""U_ItemCode"", '') <> ''
										) F
										--and E.""ItmsGrpCod"" = '101'
						where A.""DocEntry"" = {DocNo} 				
						AND TG.ItmsGrpNam like '%customer items%'
						AND C.""InvntItem"" = 'Y'
						--and isnull(B.""U_ItemCode"", '') <> ''
						";

		var res = _sql.GetData<dynamic, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text).FirstOrDefault();

		NoOfBoxes = res.NoOfBoxes ?? 0;
		FacilityLoc = res.FacilityLoc ?? "";
		IrradDate = res.IrradDate ?? DateTime.MinValue;

		return (IrradDate, NoOfBoxes, FacilityLoc);
	}

	public CertificateOfIrradiation GetCertificateOfIrradiation(string QCOrderNo)
	{
		return _certificateOfIrradiationDataService.Get(QCOrderNo);
	}

	public async Task<bool> UpdateSOStatus(int DocEntry)
	{
		try
		{

			string jsonString = $@"{{ ""U_SOStatus"" : ""Irradiated - In Storage - For COI Approval""}}";

			await _sl.PatchStringAsync("Orders", DocEntry, jsonString);

			return true;
		}
		catch (Exception)
		{

			throw;
		}
	}
}
