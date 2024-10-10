using Application.Hubs.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

public class DashboardNotifHub : Hub<IDashboardNotificationClient>
{
	public IDashboardNotificationService _eventsService;

	public DashboardNotifHub(IDashboardNotificationService eventsService)
	{
		_eventsService = eventsService;
	}

	public override Task OnConnectedAsync()
	{
		string connectionID = Context.ConnectionId;
		return base.OnConnectedAsync();
	}
	public override Task OnDisconnectedAsync(Exception? exception)
	{
		string connectionID = Context.ConnectionId;
		return base.OnDisconnectedAsync(exception);
	}

	public Task UpdateSalesOrder(int salesOrderDocNum)
	{
		var data = _eventsService.Get(salesOrderDocNum);
		return Clients.All.UpdateSalesOrder(data);
	}

	public async Task SetOngoing(string palletCode, string status)
	{
		var data = await _eventsService.SetOngoingAsync(palletCode, status);
		await Clients.All.UpdateSalesOrder(data);
	}
}