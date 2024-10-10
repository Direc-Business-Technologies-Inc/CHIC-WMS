using Application.BlazorServer.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;

namespace Application.BlazorServer.Pages.Dashboard;

public partial class DispatchNotifications : ComponentBase
{
	[Inject] private NavigationManager Navigation { get; set; }
	[Inject] IMapper _mapper { get; set; } = default!;
	[Inject] IDashboardNotificationService _dashboardNotificationService { get; set; } = default!;
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;

	dynamic Breadcrumbs = new dynamic[]
	{
		"Dashboard",
		"Dispatch Dashboard"
	};

	private HubConnection? _hubConnection;
	readonly HashSet<IDisposable> _hubRegistrations = new();

	private bool isMute { get; set; } = true;
	private bool _IsBusy { get; set; } = false;

	List<DashboardNotificationViewModel> _itemList = new();
	DashboardNotificationViewModel _item = new();

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
			GetList();

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
			if (Convert.ToDateTime(@event.DispatchDate == "" ? DateTime.MinValue.ToString() : @event.IrradiationDate).Date == DateTime.Today)
			{
				var target = _itemList.FirstOrDefault(x => x.DocNum == @event.DocNum);

				if (target is not null)
				{
					if (target.Status != @event.Status && @event.Status == "For Dispatch")
					{
						_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
					}
					_mapper.Map(@event, target);
				}
				else
				{
					if (@event.Status == "For Dispatch")
					{
						_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
					}

					DashboardNotificationViewModel newData = new DashboardNotificationViewModel();
					_mapper.Map(@event, newData);
					_itemList.Add(newData);
				}
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

	void GetList()
	{
		var data = _dashboardNotificationService.GetAll();
		//_itemList.AddRange(data);
		_itemList.AddRange(data.Where(x => Convert.ToDateTime(x.DispatchDate == "" ? DateTime.MinValue.ToString() : x.DispatchDate).Date == DateTime.Today));
	}

	public void SoundNotification(bool isMuted)
	{
		isMute = !isMuted;
		_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
		//StateHasChanged();
	}

	public async void SelectSO(DashboardNotificationViewModel SO)
	{
		if (await _jSRuntime.InvokeAsync<bool>("confirm", $"Confirm SO No. {SO.DocNum} for Dispatch?", "Proceed", "Return"))
		{
			//string status = _pallet.OngoingStatus ?? "";

			//Update to SAP
			_IsBusy = true;
			StateHasChanged();
			try
			{
				if (await _dashboardNotificationService.UpdateStatusSO(SO, "For Dispatch - Ready"))
				{
					_itemList.Where(x => x.DocNum == SO.DocNum).FirstOrDefault().Status = "For Dispatch - Ready";
					_hubConnection.UpdateSalesOrder(SO.DocNum);

					_jSRuntime.InvokeVoidAsync("HideModal");
					_IsBusy = false;
				}
			}
			catch (Exception)
			{


				_jSRuntime.InvokeVoidAsync("HideModal");
				_IsBusy = false;
				//throw;
			}
			StateHasChanged();

		}
	}
}