using Application.BlazorServer.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

namespace Application.BlazorServer.Pages.Dashboard;

public partial class DashboardNotification : ComponentBase
{
	private HubConnection? _hubConnection;
	readonly HashSet<IDisposable> _hubRegistrations = new();

	private Timer timer;
	private DateTime DigitalClock = DateTime.Now;

	private bool isMute { get; set; } = true;

	[Inject] NavigationManager Navigation { get; set; }
	[Inject] IMapper _mapper { get; set; } = default!;
	[Inject] IServiceTypeService _serviceTypeService { get; set; } = default!;
	[Inject] IDashboardNotificationService _dashboardNotificationService { get; set; } = default!;
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] IWebHostEnvironment WebHostEnvironment { get; set; }
	List<DashboardNotificationViewModel> _itemList = new();
	void GetList()
	{
		var data = _dashboardNotificationService.GetAll();
		_itemList.AddRange(data);
	}
	protected override async Task OnInitializedAsync()
	{
		try
		{
			timer = new Timer(UpdateClock,
						   null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

			//_hubConnection = new HubConnectionBuilder()
			//	.WithUrl(Navigation.ToAbsoluteUri("/hubs/dashboard-notification"))
			//	.Build();
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


			_jSRuntime.InvokeVoidAsync("showNotification");
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

			var target = _itemList.FirstOrDefault(x => x.DocNum == @event.DocNum);
			if (target is not null)
			{
				_mapper.Map(@event, target);

				//if (target.Status == "For Dispatch" || target.Status == "Dispatch" || target.Status == "Receiving" 
				if (target.Status == "For Dispatch - Ready" || target.Status == "For Receiving - Ready"
				|| target.EBStatus == "For Loading" || target.EBStatus == "Good To Load"
				|| target.Lines.Where(x => x.OngoingStatus == "At Irradiation").Count() == 1
				&& (@event.EBStatus != target.EBStatus || @event.Status != target.Status))
				{
					_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
				}
			}
			else
			{
				DashboardNotificationViewModel newData = new DashboardNotificationViewModel();
				_mapper.Map(@event, newData);
				_itemList.Add(newData);

				//if (newData.Status == "For Dispatch" || newData.Status == "Dispatch" || newData.Status == "Receiving" 
				if (newData.Status == "For Dispatch - Ready" || newData.Status == "For Receiving - Ready"
				|| newData.EBStatus == "For Loading" || newData.EBStatus == "Good To Load"
				|| newData.Lines.Where(x => x.OngoingStatus == "At Irradiation").Count() == 1)
				{
					_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
				}
			}
			StateHasChanged();
		});
	}

	async Task TestUpdate()
	{
		await _hubConnection?.UpdateSalesOrder(97);
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

	public async void UpdateClock(Object stateInfo)
	{
		await InvokeAsync(() =>
		{
			DigitalClock = DateTime.Now;
			StateHasChanged();
		});
	}

	public void SoundNotification(bool isMuted)
	{
		isMute = !isMuted;
		_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
		StateHasChanged();
	}
}
