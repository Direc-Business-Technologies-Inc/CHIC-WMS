using Application.Hubs;
using Application.Hubs.Repositories;
using Application.Models.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Application.BlazorServer.Extensions
{
    public static class ApplicationEventsConnectionExtensions
    {
        #region Client Handlers
        public static IDisposable OnUpdateSalesOrder(
        this HubConnection connection, Func<DashboardNotificationViewModel, Task> handler) =>
        connection.On(nameof(IDashboardNotificationClient.UpdateSalesOrder), handler);
        #endregion

        #region Server Calls
        public static Task UpdateSalesOrder(this HubConnection connection, int salesOrderDocNum)
        => connection.InvokeAsync(nameof(DashboardNotifHub.UpdateSalesOrder), salesOrderDocNum);

		public static Task SetOngoing(this HubConnection connection, string palletCode, string status)
		=> connection.InvokeAsync(nameof(DashboardNotifHub.SetOngoing), palletCode, status);
		#endregion
	}
}
