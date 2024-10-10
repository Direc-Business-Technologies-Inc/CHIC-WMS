using static Application.Models.ViewModels.DashboardViewModel;

namespace Application.Services.Repositories
{
    public interface IDashboardService
    {
        Task<DashboardViewModel.BinDashboardViewModel> InitalizeBinDashboard();

        void GetBinMappingDetails(DashboardViewModel.BinDashboardViewModel model);
        //List<DashboardViewModel.QuantitySpecs> GetSpecifications();
        //List<DashboardViewModel.Schedule> GetSchedules();
        //List<DashboardViewModel.IrradiationParameter> GetIrradiationParameters();
        Task<DashboardViewModel> InitializeDashboard();
        Task<DashboardViewModel> InitializeSalesOrderDetails(string SONo);
		List<SalesOrderActivity> GetSalesOrderActivities(string SONo);
        List<PalletDetails> GetPalletDetailsList(string SONo);
        List<Batch> GetBatches(string SONo);
	}
}