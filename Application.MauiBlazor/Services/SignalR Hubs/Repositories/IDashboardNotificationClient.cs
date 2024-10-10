using Application.Models.ViewModels;

namespace Application.MauiBlazor.Hubs.Repositories
{
	public interface IDashboardNotificationClient
	{
		Task UpdateSalesOrder(DashboardNotificationViewModel @event);
	}
}
