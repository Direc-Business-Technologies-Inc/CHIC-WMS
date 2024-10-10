using Application.BlazorServer.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using static Application.Models.ViewModels.DashboardViewModel;
using static Application.Models.ViewModels.QCMaintenanceViewModel;

namespace Application.BlazorServer.Pages.Dashboard
{
	public partial class SalesOrderDetails : ComponentBase
	{
		[Parameter]
		public string Id { get; set; } = string.Empty;

		dynamic Breadcrumbs = new dynamic[]
		{
			new dynamic []{
				"Dashboard",
				"../Home"
			},
			"SalesOrderDetails"
		};

		[Inject] IDashboardService _dashboardService { get; set; } = default!;
		[Inject] private NavigationManager Navigation { get; set; }
		[Inject] IMapper _mapper { get; set; } = default!;
		[Inject] IDashboardNotificationService _dashboardNotificationService { get; set; } = default!;

		private HubConnection? _hubConnection;
		readonly HashSet<IDisposable> _hubRegistrations = new();

		RadzenDataGrid<PalletDetails> grid;
		DashboardViewModel model = new();
		List<Batch> batchList = new List<Batch>();
		protected override async Task OnInitializedAsync()
		{
			try
			{

				_hubConnection = new HubConnectionBuilder()
					.WithUrl(Navigation.ToAbsoluteUri("/hubs/dashboard-notification"), (opts) =>
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
			catch (Exception)
			{

				throw;
			}
		}
		async Task UpdateSalesOrder(DashboardNotificationViewModel @event)
		{
			await InvokeAsync(() =>
			{
				if (@event.DocNum.ToString() == Id)
				{
					string SONo = @event.DocNum.ToString();
					model.SalesOrderActivities = _dashboardService.GetSalesOrderActivities(SONo);
					model.PalletDetailsList = _dashboardService.GetPalletDetailsList(SONo);
					model.Batches = _dashboardService.GetBatches(SONo);
					batchList = model.Batches;
					StateHasChanged();
				}
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

		protected override void OnInitialized()
		{
			model = _dashboardService.InitializeSalesOrderDetails(Id).Result;
			batchList = model.Batches;
		}

		void RowRender(RowRenderEventArgs<PalletDetails> args)
		{
			args.Expandable = model.Batches.Where(x => x.Activity == args.Data.Activity).Any();
		}

		void RowExpand(PalletDetails pallet)
		{
			batchList = new List<Batch>();
			batchList = model.Batches.Where(x => x.Activity == pallet.Activity && x.PalletNo == pallet.PalletNo).ToList();
		}
	}
}
