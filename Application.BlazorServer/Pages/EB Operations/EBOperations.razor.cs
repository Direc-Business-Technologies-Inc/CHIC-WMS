using Application.BlazorServer.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using static Application.Models.ViewModels.DashboardNotificationViewModel;

namespace Application.BlazorServer.Pages.EB_Operations;

public partial class EBOperations
{
	[Inject] private NavigationManager Navigation { get; set; }
	[Inject] IMapper _mapper { get; set; } = default!;
	[Inject] IDashboardNotificationService _dashboardNotificationService { get; set; } = default!;
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;

	dynamic Breadcrumbs = new dynamic[]
	{
		"QC",
		"E-beam Operations"
	};
	private HubConnection? _hubConnection;
	readonly HashSet<IDisposable> _hubRegistrations = new();

	private class EBOperation
	{
		public string Parameter { get; set; } = "";
		public double Specification { get; set; }
		public bool Passed { get; set; }
	}

	List<DashboardNotificationViewModel> _itemList = new();
	DashboardNotificationViewModel _item = new();
	private bool isMute { get; set; } = true;
	private int _atIrradPalletsCount { get; set; } = 0;
	private int _forUnloadingPalletsCount { get; set; } = 0;

	DashboardNotificationLineViewModel _pallet = new DashboardNotificationLineViewModel();

	List<EBOperation> _ebCheckList = new List<EBOperation>() {
		new EBOperation{ Parameter = "Beam Energy", Specification = 30.00, Passed = false },
		new EBOperation{ Parameter = "Beam Power", Specification = 25.70, Passed = false },
		new EBOperation{ Parameter = "Frequency", Specification = 17.90, Passed = false }
	};
	IList<EBOperation> selectedEB = new List<EBOperation>();
	RadzenDataGrid<EBOperation> EBOperationGrid;

	Dictionary<string, string> myDictionary = new Dictionary<string, string>
	{
		[""] = "For Loading",
		["For Loading"] = "For Loading Validation",
		["For Loading Validation"] = "Good To Load",
		["Good To Load"] = "At Irradiation",
		["At Irradiation"] = "Completed",
		["For Unloading"] = "Completed"
	};

	Dictionary<string, string> myDictionaryPallets = new Dictionary<string, string>
	{
		[""] = "At Irradiation",
		["At Irradiation"] = "For Unloading",
		["For Unloading"] = "Completed"
	};

	void GetList()
	{
		var data = _dashboardNotificationService.GetAll();
		//_itemList.AddRange(data);
		_itemList.AddRange(data.Where(x => Convert.ToDateTime(x.IrradiationDate == "" ? DateTime.MinValue.ToString() : x.IrradiationDate).Date == DateTime.Today));
		RecountAtIrradPallets();
	}
	void RecountAtIrradPallets()
	{
		_atIrradPalletsCount = 0;
		_forUnloadingPalletsCount = 0;

		foreach (var SO in _itemList.Where(x => x.EBStatus == "At Irradiation"))
		{
			_atIrradPalletsCount += SO.Lines.Where(x => x.OngoingStatus == "At Irradiation").Count();
			_forUnloadingPalletsCount += SO.Lines.Where(x => x.OngoingStatus == "For Unloading").Count();
		}
	}
	protected override Task OnAfterRenderAsync(bool firstRender)
	{
		return base.OnAfterRenderAsync(firstRender);
	}
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
			if (Convert.ToDateTime(@event.IrradiationDate == "" ? DateTime.MinValue.ToString() : @event.IrradiationDate).Date == DateTime.Today)
			{
				var target = _itemList.FirstOrDefault(x => x.DocNum == @event.DocNum);

				if (target is not null)
				{
					if ((@event.Status == "In Storage - For Irradiation" && string.IsNullOrWhiteSpace(@event.EBStatus))
					|| @event.EBStatus == "For Loading Validation"
					|| @event.EBStatus == "Good To Load"
					|| (@event.EBStatus == "At Irradiation" && target.Lines.Where(x => x.OngoingStatus == "At Irradiation").Count() == 0)
					|| (@event.EBStatus == "At Irradiation" && target.Lines.Where(x => x.OngoingStatus == "For Unloading").Count() == 0)
					&& (@event.EBStatus != target.EBStatus || @event.Status != target.Status))
					{
						_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
					}
					_mapper.Map(@event, target);
				}
				else
				{
					if ((@event.Status == "In Storage - For Irradiation" && string.IsNullOrWhiteSpace(@event.EBStatus))
					|| @event.EBStatus == "For Loading Validation"
					|| @event.EBStatus == "Good To Load"
					|| (@event.EBStatus == "At Irradiation" && @event.Lines.Where(x => x.OngoingStatus == "At Irradiation").Count() == 0)
					|| (@event.EBStatus == "At Irradiation" && @event.Lines.Where(x => x.OngoingStatus == "For Unloading").Count() == 0))
					{
						_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
					}

					DashboardNotificationViewModel newData = new DashboardNotificationViewModel();
					_mapper.Map(@event, newData);
					_itemList.Add(newData);
				}
				RecountAtIrradPallets();
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

	public async void InvalidSO()
	{
		if (await _jSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to mark this Sales Order as Invalid?", "Mark as Invalid", "Return"))
		{
			//Update to SAP
			if (await _dashboardNotificationService.UpdateEBStatusSO(_item, ""))
			{
				_item.EBStatus = "";
				_hubConnection.UpdateSalesOrder(_item.DocNum);
			}

			_jSRuntime.InvokeVoidAsync("HideModal");
		}
	}

	public async void ProceedNextStepSO()
	{
		if (await _jSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to proceed to the next step?", "Proceed", "Return"))
		{
			string status = _item.EBStatus ?? "";

			//Update to SAP
			if (await _dashboardNotificationService.UpdateEBStatusSO(_item, myDictionary[status]))
			{
				_item.EBStatus = myDictionary[status];
				_hubConnection.UpdateSalesOrder(_item.DocNum);
			}

			_jSRuntime.InvokeVoidAsync("HideModal");
		}
	}

	public async void ProceedNextStepPallet()
	{
		if (await _jSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to proceed to the next step?", "Proceed", "Return"))
		{
			string status = _pallet.OngoingStatus ?? "";

			//Update to SAP
			if (await _dashboardNotificationService.UpdateEBStatusPallet(_pallet, myDictionaryPallets[status]))
			{
				int allList = _itemList.Where(x => x.DocNum == _item.DocNum).FirstOrDefault().Lines.Count();
				int completedList = _itemList.Where(x => x.DocNum == _item.DocNum).FirstOrDefault().Lines.Where(x => x.OngoingStatus == "Completed").Count();

				if (allList == completedList)
				{
					await _dashboardNotificationService.UpdateEBStatusSO(_item, myDictionary[status]);
					_item.EBStatus = myDictionary[status];
				}

				_item.Lines.Where(x => x.PalletNo == _pallet.PalletNo).FirstOrDefault().OngoingStatus = myDictionaryPallets[status];
				_hubConnection.UpdateSalesOrder(_item.DocNum);
			}

			_jSRuntime.InvokeVoidAsync("HideModal");
		}
	}

	public async void SelectSO(DashboardNotificationViewModel SO, bool Passed = false)
	{
		_item = SO;
		_ebCheckList = new List<EBOperation>() {
			new EBOperation{ Parameter = "Beam Energy", Specification = Convert.ToDouble(SO.BeamEnergy), Passed = Passed },
			new EBOperation{ Parameter = "Beam Power", Specification = Convert.ToDouble(SO.BeamPower), Passed = Passed },
			new EBOperation{ Parameter = "Frequency", Specification = Convert.ToDouble(SO.Frequency), Passed = Passed }
		};

		selectedEB = null;

		if (Passed)
		{
			selectedEB = new List<EBOperation>();
			foreach (var list in _ebCheckList)
			{
				selectedEB.Add(list);
			}
		}
	}

	public async void SelectPallet(DashboardNotificationLineViewModel Pallet, bool Passed = false)
	{
		_pallet = Pallet;
		_item = _itemList.Where(x => x.DocNum == Convert.ToInt32(Pallet.PalletNo.Split("-")[0])).FirstOrDefault();
		_ebCheckList = new List<EBOperation>() {
			new EBOperation{ Parameter = "Beam Energy", Specification = Convert.ToDouble(_item.BeamEnergy), Passed = Passed },
			new EBOperation{ Parameter = "Beam Power", Specification = Convert.ToDouble(_item.BeamPower), Passed = Passed },
			new EBOperation{ Parameter = "Frequency", Specification = Convert.ToDouble(_item.Frequency), Passed = Passed }
		};

		selectedEB = null;

		if (Passed)
		{
			selectedEB = new List<EBOperation>();
			foreach (var list in _ebCheckList)
			{
				selectedEB.Add(list);
			}
		}
	}

	public void SoundNotification(bool isMuted)
	{
		isMute = !isMuted;
		_jSRuntime.InvokeVoidAsync("SoundNotification", isMute);
		StateHasChanged();
	}
}