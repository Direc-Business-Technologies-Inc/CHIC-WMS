using Application.MauiBlazor.Extensions;
using Application.MauiBlazor.Services;
using Application.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Radzen;
using Radzen.Blazor;
using static Application.Models.ViewModels.DashboardViewModel;
using static Application.Models.ViewModels.QCMaintenanceViewModel;

namespace Application.MauiBlazor.Pages;

public partial class Index
{
	[Inject] private NavigationManager Navigation { get; set; }
	[Inject] IMapper _mapper { get; set; } = default!;
	[Inject] protected IConfiguration _conf { get; set; }
	[Inject] protected RestServiceFactory _httpClientFactory { get; set; }
	private RestService _restService { get; set; }
	private HubConnection? _hubConnection;
	readonly HashSet<IDisposable> _hubRegistrations = new();

	RadzenDataGrid<DashboardNotificationViewModel> grid = new();

	List<DashboardNotificationViewModel> _itemList = new();
	IList<DashboardNotificationViewModel> selectedSalesOrder;
	//private class DataItem
	//{
	//	public string Module { get; set; }
	//	public double Count { get; set; }
	//}

	//DataItem[] records = new DataItem[]
	//{
	// new DataItem { Module = "Open Order", Count = 110 },
	// new DataItem { Module = "Receiving", Count = 54 },
	// new DataItem { Module = "Transfer", Count = 85 },
	// new DataItem { Module = "Assignment", Count = 80 }
	//};
	async Task InitializeApplicationEventsConnection(string baseAddress)
	{
		_hubConnection = new HubConnectionBuilder()
		.WithUrl(Navigation.ToAbsoluteUri(baseAddress + "hubs/dashboard-notification"), (opts) =>
		{
			opts.HttpMessageHandlerFactory = (message) =>
			{
				if (message is HttpClientHandler clientHandler)
					// bypass SSL certificate
					clientHandler.ServerCertificateCustomValidationCallback +=
						(sender, certificate, chain, sslPolicyErrors) => { return true; };
				return message;
			};
		})
		.Build();
		_hubRegistrations.Add(_hubConnection.OnUpdateSalesOrder(UpdateSalesOrder));
		await _hubConnection.StartAsync();
	}
	protected override async Task OnInitializedAsync()
	{
		string baseAddr = _conf["WebApiEndpoint"];
		_restService = _httpClientFactory.Create(baseAddr);

		string signalRAddr = _conf["SignalREndpoint"];

		InitializeApplicationEventsConnection(signalRAddr);

		var data = await _restService.Get<List<DashboardNotificationViewModel>>($"SalesOrder/GetAllDashBoardNotif");
		_itemList = data.OrderByDescending(x => x.DocNum).ToList();
		StateHasChanged();
	}
	async Task UpdateSalesOrder(DashboardNotificationViewModel @event)
	{
		await InvokeAsync(() =>
		{
			var target = _itemList.FirstOrDefault(x => x.DocNum == @event.DocNum);
			if (target is not null)
			{
				_mapper.Map(@event, target);
			}
			else
			{
				DashboardNotificationViewModel newData = new DashboardNotificationViewModel();
				_mapper.Map(@event, newData);
				_itemList.Add(newData);
				_itemList.OrderByDescending(x => x.DocNum);
			}
			StateHasChanged();
		});
	}

	public bool IsConnected =>
		_hubConnection?.State == HubConnectionState.Connected;

	public async ValueTask DisposeAsync()
	{
		if (_hubRegistrations is { Count: > 0 })
		{
			foreach (var disposable in _hubRegistrations)
			{
				disposable.Dispose();
			}
		}

		if (_hubConnection is not null)
		{
			await _hubConnection.DisposeAsync();
		}
	}

	void RowRender(RowRenderEventArgs<DashboardNotificationViewModel> args)
	{
		//args.Expandable = model.Batches.Where(x => x.Activity == args.Data.Activity).Any();
	}

	void RowExpand(DashboardNotificationViewModel pallet)
	{
		//batchList = new List<Batch>();
		//batchList = model.Batches.Where(x => x.Activity == pallet.Activity && x.PalletNo == pallet.PalletNo).ToList();
	}
}