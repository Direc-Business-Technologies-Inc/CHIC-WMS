using Radzen.Blazor.Rendering;
using System.Reflection.PortableExecutable;

namespace Application.BlazorServer.Pages.FormsAndReports;

public partial class FormsAndReports : ComponentBase
{
    [Inject] protected IJSRuntime _jsRuntime { get; set; } = default!;
    [Inject] private IConfiguration _configuration { get; set; }
    [Inject] private IFormsAndReportsService _formsAndReportsService { get; set; }
    [Inject] private IWebHostEnvironment _environment { get; set; }
    FormsAndReportsViewModel model = new FormsAndReportsViewModel();

    dynamic Breadcrumbs = new dynamic[]
    {
        "Forms And Reports"
    };

    private string _SONo { get; set; } = string.Empty;
    private string searchString { get; set; } = string.Empty;
    private string ReportName { get; set; } = string.Empty;
    private string Type { get; set; } = string.Empty;
    private DateTime _DocDate { get; set; } = DateTime.Today;
	RadzenButton button;
	Popup popup;
	RadzenDataList<FormsAndReportsViewModel.SalesOrderDetail> dataList;
	protected override void OnInitialized()
    {
        model = _formsAndReportsService.InitializeFormsAndReports();
        model.Reports.AddRange(new List<FormsAndReportsViewModel.Report>
        {
            new FormsAndReportsViewModel.Report { Type = "Schedules", Name = "Irradiation Schedule", FileName = "Irradiation Schedule" }
            ,new FormsAndReportsViewModel.Report { Type = "Schedules", Name = "Daily Receiving Report", FileName = "Daily Receiving Report" }
            ,new FormsAndReportsViewModel.Report { Type = "Schedules", Name = "Dispatch Schedule", FileName = "Dispatch Schedule" }
            ,new FormsAndReportsViewModel.Report { Type = "Schedules", Name = "Receiving Schedule", FileName = "Receiving Schedule" }
            ,new FormsAndReportsViewModel.Report { Type = "Schedules", Name = "Daily Dispatch Report", FileName = "Daily Dispatch Report" }
            ,new FormsAndReportsViewModel.Report { Type = "Schedules", Name = "Daily Irradiation Report", FileName = "Daily Irradiation Report" }
            ,new FormsAndReportsViewModel.Report { Type = "Documents", Name = "Dispatch Form / Acknowledgement Receipt", FileName = "Acknowledgement Receipt" }
            ,new FormsAndReportsViewModel.Report { Type = "Documents", Name = "Product and Process Specification Sheet", FileName = "Product and Process Specs" }
            ,new FormsAndReportsViewModel.Report { Type = "Documents", Name = "SO Report", FileName = "SO Report V2" }
        });
    }

    public void SelectInput(string value)
    {
        _SONo = value;
        _jsRuntime.InvokeVoidAsync("HideModal");
    }

    public async Task PrintReport()
    {
        try
        {
            if (Type == "Documents" && _SONo == "")
            {
                //INSERT BLOCKERS
                _jsRuntime.InvokeVoidAsync("ShowResult", "Warning", "Please select a Parameter.");
                //_jsRuntime.InvokeVoidAsync("ShowResult", "Warning", "Please select Sales Order.");
                return;
            }

            string Parameter = Type == "Documents" ? _SONo : _DocDate.ToString("MM/dd/yyyy");

            //string filePath = $"{_environment.WebRootPath}/PRINT_LAYOUT/Summary of Irradiation Schedule.rpt";
            string filePath = $"{_environment.WebRootPath}/PRINT_LAYOUT/{ReportName}.rpt";

            //string url = $"http://localhost:44308/api/Print?Header={Header}&args={args}&PrinterName={string.Empty}&FilePath={FilePath}&Database={Database}";
            string endpoint = _configuration["PrinterAPI"]?.ToString() ?? "";
            string url = $"{endpoint}/api/Print?FilePath={filePath.Replace("\\", "/")}&Parameter={Parameter}&Type={Type}";

            // JavaScript to open the report in a new window
            string script = "var newWindow = window.open(`" + url + "`, '_blank');";
            script += "newWindow.focus();";

            // Execute the JavaScript
            await _jsRuntime.InvokeVoidAsync("eval", script);

            _SONo = string.Empty;

		}
        catch (Exception)
        {

            throw;
        }
    }
    
    async Task SelectOrder(FormsAndReportsViewModel.SalesOrderDetail order)
	{
		_SONo = order.SONo;
		await popup.CloseAsync();
	}

	async Task OnOpen()
	{
		await _jsRuntime.InvokeVoidAsync("eval", "setTimeout(function(){ document.getElementById('search').focus(); }, 200)");
	}
}
