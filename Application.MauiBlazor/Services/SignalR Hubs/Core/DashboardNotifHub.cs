using Application.MauiBlazor.Hubs.Repositories;
using Application.MauiBlazor.Services;
using Application.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace Application.MauiBlazor.Hubs
{
	public class DashboardNotifHub : Hub<IDashboardNotificationClient>
	{
		////INSERT API
		private RestServiceFactory _httpClientFactory;
		public IConfiguration _conf;
		private RestService _restService;

		public DashboardNotifHub(RestServiceFactory httpClientFactory, IConfiguration conf)
		{
			_conf = conf;
			_httpClientFactory = httpClientFactory;

			string baseAddr = _conf["WebApiEndpoint"];
			_restService = _httpClientFactory.Create(baseAddr);
		}
		public Task UpdateSalesOrder(int salesOrderDocNum)
		{
			var data = _restService.Get<DashboardNotificationViewModel>($"SalesOrder/UpdateSalesOrder?salesOrderDocNum={salesOrderDocNum}");
			return Clients.All.UpdateSalesOrder(data.Result);
		}
	}
}