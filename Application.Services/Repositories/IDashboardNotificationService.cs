using DataManager.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Models.ViewModels.DashboardNotificationViewModel;

namespace Application.Services.Repositories
{
    public interface IDashboardNotificationService : IGenericAsyncService<DashboardNotificationViewModel>, IGenericService<DashboardNotificationViewModel>
    {
        Task<DashboardNotificationViewModel> UpdateAsync(DashboardNotificationViewModel data);
        Task<DashboardNotificationViewModel> SetOngoingAsync(string palletCode, string status);
        Task<bool> UpdateEBStatusSO(DashboardNotificationViewModel Item, string Status);
        Task<bool> UpdateEBStatusPallet(DashboardNotificationLineViewModel Item, string Status);
        Task<List<DashboardNotificationViewModel>> GetAllSODashboardMobile();
        Task<bool> UpdateStatusSO(DashboardNotificationViewModel Item, string Status);

	}
}
