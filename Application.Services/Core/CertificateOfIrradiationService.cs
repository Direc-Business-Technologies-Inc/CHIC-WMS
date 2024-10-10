using DataManager.Models.QCOrder;
using System.Reflection.Metadata;
using static Application.Models.ViewModels.CertificateOfIrradiationViewModel;

namespace Application.Services.Core;

public class CertificateOfIrradiationService : ICertificateOfIrradiationService
{
	private readonly IMsSqlDataAccess _sql;
	private readonly IServiceLayerDataAccess _sl;
	private readonly IQCOrderDataService _dataQCOrder;
	private readonly IQCMaintenanceDataService _dataQCMaintenance;
	private readonly ICertificatOfIrradiationDataService _dataCOI;
	private readonly IMapper Mapper;

	public CertificateOfIrradiationService(IConfiguration configuration, IMapper mapper, IQCOrderDataService qCOrder, ICertificatOfIrradiationDataService certificatOfIrradiationDataService, IQCMaintenanceDataService dataQCMaintenance, IServiceLayerDataAccess sl)
	{
		_sql = new MsSqlDataAccess(configuration);
		Mapper = mapper;
		_dataQCOrder = qCOrder;
		_dataCOI = certificatOfIrradiationDataService;
		_dataQCMaintenance = dataQCMaintenance;
		_sl = sl;
	}
	public CertificateOfIrradiationViewModel InitializeCertificateOfIrradiation()
	{
		try
		{
			//var qcorderList = _dataQCOrder.GetQCOrderList().Where(x => x.InspectionPlanType == "Dosimetry Quality Control Order" && x.Status == "Passed");

			//var Parameter = qcorderList.Select(x => x.DocNo.ToString()).ToArray();
			//string Parameters = string.Join(", ", Parameter);

			//string qry = $"SELECT DocEntry,U_IrridiationDate FROM ORDR Where DocEntry IN({Parameters})";

			//var ScheduleDates = _sql.GetData<Schedules, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			CertificateOfIrradiationViewModel model = new CertificateOfIrradiationViewModel();

			var COI = _dataCOI.Get();

			var DocNoList = COI.Select(x => x.DocNo).ToArray();
			string Parameters = string.Join(", ", DocNoList);

			if (!string.IsNullOrEmpty(Parameters))
			{
				string qry = $@"select T0.DocEntry as ""DocNo"" from ORDR T0
					Inner Join ""@SERVICE_DATA_ROW"" T1
					on T0.U_ServiceType = T1.Code
					where T0.DocEntry IN({Parameters})
					and T1.U_TransferType = 'At Irradiation'
					and (Select top 1 'true' from OBTN T2 where T2.U_SONo = T0.DocEntry and T2.U_TIMS_ITSortCode >= T1.U_SortCode) = 'true'";

				List<COISalesOrderDetails> IrradiatedSOList = _sql.GetData<COISalesOrderDetails, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);
				//foreach (var qcorder in qcorderList)
				foreach (var qcorder in COI.Where(x => IrradiatedSOList.Where(y => y.DocNo == x.DocNo).Any()))
				{
					model.COISalesOrderList.Add(new CertificateOfIrradiationViewModel.COISalesOrderDetails
					{
						QCOrderNo = qcorder.QCOrderNo,
						DocNo = qcorder.DocNo,
						CustomerName = qcorder.CustomerName,
						ItemName = qcorder.ItemName,
						IrradiationDate = qcorder.IrradiationDate,
						//IrradiationDate = ScheduleDates.Where(x => x.DocEntry == qcorder.DocNo).FirstOrDefault().U_IrridiationDate,
						Status = qcorder.Status
						//Status = COI.Where(x => x.QCOrderNo == qcorder.QCOrderNo).Any() ? COI.Where(x => x.QCOrderNo == qcorder.QCOrderNo).FirstOrDefault().Status : "For Approval"
					});
				}
			}

			return model;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<CertificateOfIrradiationViewModel> InitializeCertificateOfIrradiationDetails()
	{
		CertificateOfIrradiationViewModel model = new CertificateOfIrradiationViewModel();

		await Task.Run(() =>
		{
			List<DataManager.Models.QCMaintenance.InspectionPlan> inspectionPlans = _dataQCMaintenance.GetQCMaintenance();

			model.InspectionPlanList = Mapper.Map<List<CertificateOfIrradiationViewModel.InspectionPlan>>(inspectionPlans);
		});

		return model;
	}

	public async Task<CertificateOfIrradiationViewModel.QCOrderDetail> SelectQCOrder(string QCOrderNo)
	{
		try
		{
			QCOrder qcOrder = _dataQCOrder.GetQCOrder(QCOrderNo);

			return Mapper.Map<CertificateOfIrradiationViewModel.QCOrderDetail>(qcOrder);

		}
		catch (Exception)
		{
			throw;
		}
	}

	public List<CertificateOfIrradiationViewModel.ParameterDetail> GetParameters(string InspectionPlanCode, string Version)
	{
		var ParameterList = _dataQCMaintenance.GetInspectionPlanParameters(InspectionPlanCode, Version);
		return Mapper.Map<List<CertificateOfIrradiationViewModel.ParameterDetail>>(ParameterList);
	}

	public async Task<CertificateOfIrradiationViewModel.COISalesOrderDetails> GetCOIDetails(string QCOrderNo)
	{
		try
		{
			CertificateOfIrradiationViewModel.COISalesOrderDetails model = new CertificateOfIrradiationViewModel.COISalesOrderDetails();

			await Task.Run(() =>
			{
				var COI = _dataCOI.Get(QCOrderNo);
				model = Mapper.Map<CertificateOfIrradiationViewModel.COISalesOrderDetails>(COI);
			});

			return model;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<bool> UpdateCOI(CertificateOfIrradiationViewModel.COISalesOrderDetails model)
	{
		try
		{
			await Task.Run(() =>
			{
				_dataCOI.Patch(
					new DataManager.Models.CertificateOfIrradiation.CertificateOfIrradiation
					{
						CertificateOfIrradiationNumber = model.CertificateOfIrradiationNumber,
						ApproverRemarks = model.ApproverRemarks,
						Status = model.Status
					});
			});

			return true;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<bool> UpdateSOStatus(int DocEntry)
	{
		try
		{

			//string jsonString = $@"{{ ""U_SOStatus"" : ""Irradiated - In Storage - Ready for Dispatch""}}";

			//await _sl.PatchStringAsync("Orders", DocEntry, jsonString);

			return true;
		}
		catch (Exception)
		{

			throw;
		}
	}
}
