namespace Application.BlazorServer.Pages.Schedules;

public partial class IrradiationSchedule : ComponentBase
{
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] private NavigationManager _navManager { get; set; }

	[Inject] private IScheduleService _scheduleService { get; set; }
	[Inject] private IFacilityLocationService _facilityLocationService { get; set; } = default!;
    private IJSObjectReference _js { get; set; } = default!;

	dynamic Breadcrumbs = new dynamic[]
	{
		"Schedules",
		"Irradiation Schedule"
	};

	ScheduleViewModel model = new ScheduleViewModel();

	List<FacilityLocationViewModel> facilityLocations = new();

	string selectedFacility = string.Empty;

	//string IrradiationDate = $"{DateTime.Today.ToString("MM-dd-yyyy")} - {DateTime.Today.ToString("MM-dd-yyyy")}";
	private DateTime startDate { get; set; } = DateTime.Today.AddDays(-5);
	private DateTime endDate { get; set; } = DateTime.Today;
	private string searchValue { get; set; } = string.Empty;
	List<ScheduleViewModel.Schedules> tableData { get; set; } = new();
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/IrradiationSchedule.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("initializeDateRange", DotNetObjectReference.Create(this));
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
			await _js.InvokeVoidAsync("loadCalendar");
		}
	}
	protected override void OnInitialized()
	{
		try
		{
			model = _scheduleService.InitializeIrradiationSchedule();
			tableData = model.ScheduleList;
		}
		catch (Exception)
		{
			throw;
		}
		facilityLocations = _facilityLocationService.GetAll();
		if(facilityLocations.Count > 0)
		{
			selectedFacility = facilityLocations[0].Code;
		}
    }

	public async Task SelectSchedule(string SONo, string ItemCode)
	{
		string ParentPage = "Irradiation";
		_navManager.NavigateTo($"ScheduleDetails/{SONo}/{ItemCode}/{ParentPage}");
	}

	[JSInvokable("FilterSchedule")]
	public async Task FilterSchedule(string start, string end, string type)
	{
		startDate = Convert.ToDateTime(start);
		endDate = Convert.ToDateTime(end);

		_scheduleService.FilterSchedule(model, start, end, type);

        if (searchValue != "")
        {
            searchValue = searchValue.ToLower();
            tableData = model.ScheduleList.Where(
                x =>
                    x.DeliveryDate.ToString("M MM MMM MMMM dd ddd dddd y yy yyy yyyy yyyyy h hh H HH m mm ss").ToLower().Contains(searchValue) ||
                    x.DocEntry.ToString().ToLower().Contains(searchValue) ||
                    x.CardName.ToLower().Contains(searchValue) ||
                    x.ItemName.ToLower().Contains(searchValue)
            // 12-3-2023 : CM : Uncomment this to add FacilityLocationCode as one of the searchable data; Make sure FacilityLocationCode does not equal to null hence will error
            //x.FacilityLocationCode.ToLower().Contains(SearchValue)
            ).ToList();
        }
        else
        {
            tableData = model.ScheduleList;
        }

        StateHasChanged();
	}

	[JSInvokable("getAllSchedules")]
	public async Task<ScheduleViewModel> getAllSchedules()
	{
		return _scheduleService.getAllSchedules(selectedFacility);
	}

	[JSInvokable("updateSchedule")]
	public async Task updateSchedule(ScheduleEvents scheduleEvents)
	{
		try
		{
			if (_scheduleService.updateEventSchedule(scheduleEvents))
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
			await Task.Delay(1000);
				_navManager.NavigateTo("IrradiationSchedule", true);
			}
			else
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
			}
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
		}
	}

	[JSInvokable("approveEventsSchedule")]
	public async Task approveEventsSchedule(List<ScheduleEvents> scheduleEvents)
	{
		try
		{
			bool result = await _scheduleService.updateEventsSchedule(scheduleEvents);
			if (result)
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Approved Succesfully");
				await Task.Delay(1000);
				_navManager.NavigateTo("IrradiationSchedule", true);
			}
			else
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
			}
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
		}
	}

	public void Clear()
	{
		model = new ScheduleViewModel();
		//IrradiationDate = $"{DateTime.Today.ToString("MM-dd-yyyy")} - {DateTime.Today.ToString("MM-dd-yyyy")}";
		startDate = DateTime.Today.AddDays(-5);
		endDate = DateTime.Today;
		StateHasChanged();
	}

	private void SearchIrradScheds(String SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			tableData = model.ScheduleList.Where(
				x =>
					x.DeliveryDate.ToString("M MM MMM MMMM dd ddd dddd y yy yyy yyyy yyyyy h hh H HH m mm ss").ToLower().Contains(SearchValue) ||
					x.DocEntry.ToString().ToLower().Contains(SearchValue) ||
					x.CardName.ToLower().Contains(SearchValue) ||
					x.ItemName.ToLower().Contains(SearchValue)
					// 12-3-2023 : CM : Uncomment this to add FacilityLocationCode as one of the searchable data; Make sure FacilityLocationCode does not equal to null hence will error
					//x.FacilityLocationCode.ToLower().Contains(SearchValue)
			).ToList();
		}
		else
		{
			tableData = model.ScheduleList;
		}
	}

	private async Task ApproveAll()
	{
		try
		{
			await _js.InvokeVoidAsync("approveAll");
		}
		catch (Exception)
		{

			throw;
		}
	}
}
