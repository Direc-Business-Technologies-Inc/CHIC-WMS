namespace Application.Hubs.Repositories;

public interface IDashboardNotificationClient
{
	Task UpdateSalesOrder(DashboardNotificationViewModel @event);
}
