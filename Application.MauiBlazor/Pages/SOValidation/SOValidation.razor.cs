using Application.MauiBlazor.Services;
using Application.MauiBlazor.Shared;
using AutoMapper;
using BlazorStrap;
using DataManager.Models.DashboardNotification;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace Application.MauiBlazor.Pages.SOValidation;

public partial class SOValidation
{
	[CascadingParameter]
	protected SneatHorizontalLayout? _layout { get; set; }
	[Inject]
	protected IBlazorStrap _blazorStrap { get; set; } = default!;
	[Inject] protected IConfiguration _conf { get; set; }
	[Inject] protected RestServiceFactory _httpClientFactory { get; set; }
	[Inject] protected IMapper _mapper { get; set; } = default!;
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] NavigationManager Navigation { get; set; } = default!;
	private HubConnection? _hubConnection;
	private RestService _restService { get; set; }
	string _scanPalletInput { get; set; } = string.Empty;
	private bool isGranted = false;
	private bool _IsBusy = false;

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

		await _hubConnection.StartAsync();
	}
	protected override void OnInitialized()
	{
		string baseAddr = _conf["WebApiEndpoint"];
		_restService = _httpClientFactory.Create(baseAddr);

		string signalRAddr = _conf["SignalREndpoint"];

		InitializeApplicationEventsConnection(signalRAddr);
	}
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			//GET CAMERA PERMISSION
			var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
			if (status == PermissionStatus.Granted)
			{
				isGranted = true;
			}
			else
			{
				status = await Permissions.RequestAsync<Permissions.Camera>();
				if (status == PermissionStatus.Granted)
				{
					isGranted = true;
				}
			}

			if (isGranted)
			{
				StateHasChanged();
			}
		}
	}

	private async void OnScanInputEnter(KeyboardEventArgs e)
	{
		if (e.Key == "Enter" || e.Key == "NumpadEnter")
		{
			if (await _jSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to validate this Sales Order?", "Validate", "Return"))
			{
				await HandleEnter();
			}
		}
	}

	private async Task HandleEnter()
	{
		_IsBusy = true;
		try
		{
			var value = _scanPalletInput;
			if (string.IsNullOrEmpty(value)) return;

			//GET THE SO WITH ITS CORRESPONDING PALLETS IF EXISTING
			DashboardNotificationModel model = new DashboardNotificationModel();

			//POST TO EBSTATUS IN SO VIA API
			//bool allIsReleased = await _restService.Post($"SalesOrder/EBStatusValidate?id={value.Split("-")[0]}");

			try
			{
				var data = await _restService.Post<bool>($"SalesOrder/EBStatusValidate?id={value.Split("-")[0]}");
				try
				{

				//SEND TO DASHBOARD VIA SIGNALR
				_hubConnection.InvokeAsync("UpdateSalesOrder", Convert.ToInt32(value.Split("-")[0]));
				}
				catch (Exception)
				{

					throw;
				}

				_blazorStrap.Toaster.Add("Success", o =>
				{
					o.Color = BSColor.Success;
					o.CloseAfter = 3000;
					o.Toast = Toast.TopRight;
				});
			}
			catch (Exception err)
			{
				_IsBusy = false;
				_blazorStrap.Toaster.Add(err.Message, o =>
				{
					o.Color = BSColor.Danger;
					o.CloseAfter = 3000;
					o.Toast = Toast.TopRight;
				});
			}
		}
		catch (Exception err)
		{
			_IsBusy = false;
			_blazorStrap.Toaster.Add(err.Message, o =>
			{
				o.Color = BSColor.Danger;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});
		}
		_IsBusy = false;
		StateHasChanged();
	}
	private void ScanResult(string data)
	{
		try
		{
			_scanPalletInput = data;
		}
		catch (Exception e)
		{
			_blazorStrap.Toaster.Add("Invalid QR", o =>
			{
				o.Color = BSColor.Danger;
				o.CloseAfter = 3000;
				o.Toast = Toast.TopRight;
			});
		}
		finally
		{
			_layout.ToggleBarcode();
		}

	}
}