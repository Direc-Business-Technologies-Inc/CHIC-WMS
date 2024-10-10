using DataManager.Models.DashboardNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataManager.Models.DashboardNotification.DashboardNotificationModel;

namespace DataManager.Services.Repositories
{
    public interface IDashboardNotificationDataService : IGenericService<DashboardNotificationModel>
    {
		Task<bool> UpdateEBStatusSO(DashboardNotificationModel Item, string Status);
		Task<bool> UpdateEBStatusPallet(DashboardNotificationLineModel Item, string Status);
		Task<List<DashboardNotificationModel>> GetAllSODashboardMobile();
		Task<bool> UpdateStatusSO(DashboardNotificationModel Item, string Status);
	}
}
