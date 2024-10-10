namespace Application.Services.Repositories
{
    public interface IQCMaintenanceService
    {
        QCMaintenanceViewModel InitializeQCMaintenance();
        bool SaveQCMaintenance(QCMaintenanceViewModel model);
		QCMaintenance GetInspectionPlan(string Code);
		QCMaintenance GetInspectionPlan(string ItemCode, string PlanType);
		QCMaintenance GetInspectionPlanWithVersion(string Code, string Version);
		List<QCMaintenanceViewModel.Version> GetVersions(string Code);
	}
}