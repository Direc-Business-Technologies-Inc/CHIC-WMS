namespace Application.BlazorServer.Pages.Schedules;

public partial class DispatchSchedule : ComponentBase
{
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] private NavigationManager _navManager { get; set; }
	[Inject] private IScheduleService _scheduleService { get; set; }
	private IJSObjectReference _js { get; set; } = default!;

	dynamic Breadcrumbs = new dynamic[]
	{
		"Schedules",
		"Dispatch Schedule"
	};

	ScheduleViewModel model = new ScheduleViewModel();
	private string searchValue { get; set; } = string.Empty;
	private DateTime startDate { get; set; } = DateTime.Today.AddDays(-5);
	private DateTime endDate { get; set; } = DateTime.Today;
	List<ScheduleViewModel.Schedules> tableData { get; set; } = new();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/DispatchSchedule.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("initializeDateRange", DotNetObjectReference.Create(this));
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
		}
	}

	protected override void OnInitialized()
	{
		try
		{
			model = _scheduleService.InitializeDispatchSchedule();
			tableData = model.ScheduleList;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task SelectSchedule(string SONo, string ItemCode)
	{
		string ParentPage = "Dispatch";
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

	private void  SearchDispatchScheds(String SearchValue) 
	{
		if(SearchValue != "")
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
}
