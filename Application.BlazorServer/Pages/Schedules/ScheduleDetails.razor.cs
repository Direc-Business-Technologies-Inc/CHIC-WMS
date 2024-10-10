namespace Application.BlazorServer.Pages.Schedules;

public partial class ScheduleDetails : ComponentBase
{
    [Parameter]
    public string ScheduleId { get; set; } = "";
    [Parameter]
    public string ItemCode { get; set; } = "";
    [Parameter]
    public string ParentPage { get; set; } = "";

    [Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
    [Inject] private NavigationManager _navManager { get; set; }

    [Inject] private IScheduleService _scheduleService { get; set; }

    private IJSObjectReference _js { get; set; } = default!;

    dynamic Breadcrumbs;

    ScheduleViewModel model = new ScheduleViewModel();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/ScheduleDetails.js");

            //For Calling of backend function from JS
            await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
        }
    }

    protected override void OnInitialized()
    {
        Breadcrumbs = new dynamic[]
        {
                "Schedules",
                new dynamic []{
                $"{ParentPage} Schedule",
                $"/{ParentPage}Schedule"
                },
                "Schedule Details"
        };

        try
        {
            model = _scheduleService.InitializeScheduleDetails(ScheduleId, ItemCode);
        }
        catch (Exception)
        {
            throw;
        }
    }

    void CellRender(DataGridCellRenderEventArgs<Items> args)
    {
        if (args.Column.Property == "Status")
        {


            if (args.Data.Status == "Tentative")
            {
                args.Attributes.Add("style", $"background-color: #FF3E1D;");
            }
            else if (args.Data.Status == "Scheduled")
            {
                args.Attributes.Add("style", $"background-color: #71DD37;");
            }
        }
    }

    async Task PatchSchedule()
    {
        if (!await _jSRuntime.InvokeAsync<bool>("confirm", "Update schedule?"))
        {
            return;
        }

        try
        {
            if (await _scheduleService.updateScheduleDetails(ScheduleId,
                model.ScheduleDetails.U_PickUpDate.ToString("yyyy-MM-dd"),
                model.ScheduleDetails.U_IrridiationDate.ToString("yyyy-MM-dd"),
                model.ScheduleDetails.U_IrridiationStart.ToString("HH:mm:ss"),
                model.ScheduleDetails.U_IrridiationEnd.ToString("HH:mm:ss"),
                model.ScheduleDetails.Remarks))
            {
                await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
                await Task.Delay(1000);
                //_navManager.NavigateTo($"ScheduleDetails/{ScheduleId}/{ParentPage}");
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

    [JSInvokable("durationChangeBackend")]
    public async Task durationChangeBackend(object newEndDate)
    {
        try
        {
            string dateString = newEndDate.ToString();
            DateTime newDate = DateTime.ParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal);
            model.ScheduleDetails.U_IrridiationEnd = newDate;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
