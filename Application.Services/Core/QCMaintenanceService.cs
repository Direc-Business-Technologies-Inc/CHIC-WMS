using DataManager.Models.Configurations;
using System.Reflection;

namespace Application.Services.Core;

public class QCMaintenanceService : IQCMaintenanceService
{
	private readonly IMapper Mapper;
	private readonly IMsSqlDataAccess _sql;
	private readonly IQCMaintenanceDataService _dataQCMaintenance;
	public QCMaintenanceService(IConfiguration configuration, IMapper mapper, IQCMaintenanceDataService dataQCMaintenance)
	{
		_sql = new MsSqlDataAccess(configuration);
		Mapper = mapper;
		_dataQCMaintenance = dataQCMaintenance;
	}

	public QCMaintenanceViewModel InitializeQCMaintenance()
	{
		try
		{
			QCMaintenanceViewModel model = new QCMaintenanceViewModel();

			List<DataManager.Models.QCMaintenance.InspectionPlan> inspectionPlans = _dataQCMaintenance.GetQCMaintenance();

			model.InspectionPlanList = Mapper.Map<List<InspectionPlans>>(inspectionPlans);

			model.DosimeterLocationList = new List<DosimeterLocation>
			{
			new DosimeterLocation { LocationName = "Min Dose Position"},
			new DosimeterLocation { LocationName = "Max Dose Position"},
			new DosimeterLocation { LocationName = "Reference Dose Position"},
			};

			string qry = $@"select ""Code"" as ""EmployeeCode"", CONCAT(""firstName"", ' ', COALESCE(""middleName"", ''), ' ', ""lastName"") AS ""EmployeeName"" from OHEM";

			model.EmployeeList = _sql.GetData<Employees, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			//qry = $@"select ""ItemCode"", ""ItemName"" from OITM WHERE ""ItmsGrpCod"" IN ('100','109')";
			qry = $@"select ""ItemCode"", ""ItemName"" from OITM T0 INNER JOIN OITB T1 ON T0.""ItmsGrpCod"" = T1.""ItmsGrpCod"" WHERE ""ItmsGrpNam"" like '%customer items%' AND ""InvntItem"" = 'Y'; ";

			model.ItemList = _sql.GetData<Item, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			qry = $@"select ""CardCode"", ""CardName"" from OCRD WHERE ""CardType"" = 'C'";

			model.CustomerList = _sql.GetData<Customers, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			qry = $@"select ""UgpCode"", ""UgpName"" from OUGP";
			model.UoMList = _sql.GetData<UoM, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			UoM item = new UoM();
			item.UgpCode = "N/A";
			item.UgpName = "N/A";

			model.UoMList.Insert(0, item);

			var QCConfigItems = _dataQCMaintenance.GetDefaultParameters();

			model.DefaultParameterList = ConvertQCMaintenanceConfigItemstoParameters(QCConfigItems);

			return model;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public bool SaveQCMaintenance(QCMaintenanceViewModel model)
	{
		try
		{
			DataManager.Models.QCMaintenance.InspectionPlan inspectionPlan = Mapper.Map<DataManager.Models.QCMaintenance.InspectionPlan>(model.QcMaintenance);

			//inspectionPlan.ParameterList = inspectionPlan.ParameterList.Where(x => x.Weight > 0).ToList();

			_dataQCMaintenance.PostQCMaintenance(inspectionPlan);
			return true;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public QCMaintenance GetInspectionPlan(string Code)
	{
		try
		{
			QCMaintenance model = new QCMaintenance();
			DataManager.Models.QCMaintenance.InspectionPlan inspectionPlan = _dataQCMaintenance.GetQCMaintenance(Code);
			model = Mapper.Map<QCMaintenance>(inspectionPlan);

			int RowCount = 1;

			foreach (var param in model.ParameterList)
			{
				param.SelectId = $"select-uom-{RowCount}";
				RowCount++;
			}


			return model;
		}
		catch (Exception)
		{
			throw;
		}
	}
	public QCMaintenance GetInspectionPlan(string ItemCode, string PlanType)
	{
		try
		{
			QCMaintenance model = new QCMaintenance();
			DataManager.Models.QCMaintenance.InspectionPlan inspectionPlan = _dataQCMaintenance.GetQCMaintenance(ItemCode, PlanType);
			if (inspectionPlan != null)
			{
				model = Mapper.Map<QCMaintenance>(inspectionPlan);

				int RowCount = 1;

				foreach (var param in model.ParameterList)
				{
					param.SelectId = $"select-uom-{RowCount}";
					RowCount++;
				}

				return model;
			}

			return null;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public QCMaintenance GetInspectionPlanWithVersion(string Code, string Version)
	{
		try
		{
			QCMaintenance model = new QCMaintenance();
			DataManager.Models.QCMaintenance.InspectionPlan inspectionPlan = _dataQCMaintenance.GetQCMaintenanceWithVersion(Code, Version);
			model = Mapper.Map<QCMaintenance>(inspectionPlan);

			int RowCount = 1;

			foreach (var param in model.ParameterList)
			{
				param.SelectId = $"select-uom-{RowCount}";
				RowCount++;
			}


			return model;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public List<QCMaintenanceViewModel.Version> GetVersions(string Code)
	{
		try
		{
			QCMaintenanceViewModel model = new QCMaintenanceViewModel();
			List<DataManager.Models.QCMaintenance.InspectionPlan> inspectionPlans = _dataQCMaintenance.GetVersionList(Code);

			foreach (var inspectionPlan in inspectionPlans)
			{
				model.VersionList.Add(new QCMaintenanceViewModel.Version { VersionNumber = inspectionPlan.Version });
			}
			//model = Mapper.Map<QCMaintenance>(inspectionPlan);

			//int RowCount = 1;

			//foreach (var param in model.ParameterList)
			//{
			//	param.SelectId = $"select-uom-{RowCount}";
			//	RowCount++;
			//}


			return model.VersionList;
		}
		catch (Exception)
		{

			throw;
		}
	}

	private List<Parameters> ConvertQCMaintenanceConfigItemstoParameters(List<ConfigurationItems> model)
	{
		try
		{
			Type type = typeof(Parameters);

			List<Parameters> items = new List<Parameters>();

			foreach (var submodel in model.DistinctBy(x => x.SubGroup))
			{
				Parameters item = new Parameters();

				foreach (var modelitem in model.Where(x => x.SubGroup == submodel.SubGroup))
				{
					PropertyInfo property = type.GetProperty(modelitem.ItemName);

					if (property == null)
						continue;

					property.SetValue(item, modelitem.ItemValue);
				}

				items.Add(item);
			}

			return items;
		}
		catch (Exception)
		{

			throw;
		}
	}
}
