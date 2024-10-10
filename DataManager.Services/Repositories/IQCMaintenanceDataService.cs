using DataManager.Models.QCMaintenance;
using System.Runtime.InteropServices;

namespace DataManager.Services.Repositories;

public interface IQCMaintenanceDataService
{
	InspectionPlan PostQCMaintenance(InspectionPlan model);
	InspectionPlan GetQCMaintenance(string InspectionPlanCode);
	InspectionPlan GetQCMaintenance(string ItemCode, string PlanType);
	InspectionPlan GetQCMaintenanceWithVersion(string InspectionPlanCode, string Version);
	InspectionPlan GetQCMaintenanceByItem(string ItemCode, [Optional] string Type);
    List<InspectionPlan> GetQCMaintenance();
	List<InspectionPlanParameter> GetInspectionPlanParameters(string InspectionPlanCode, string Version);
	List<InspectionPlan> GetVersionList(string InspectionPlanCode);
	List<ConfigurationItems> GetDefaultParameters();
}