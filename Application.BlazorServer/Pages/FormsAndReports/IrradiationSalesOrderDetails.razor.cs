using Application.Libraries.SAP;
using Microsoft.EntityFrameworkCore;

namespace Application.BlazorServer.Pages.FormsAndReports;

public partial class IrradiationSalesOrderDetails : ComponentBase
{
	[Parameter]
	public string SONo { get; set; } = "";

	[Inject] private IFormsAndReportsService _formsAndReportsService { get; set; }

    [Inject] private IConfiguration _configuration { get; set; }
    [Inject] private IWebHostEnvironment _environment { get; set; }
	[Inject] private IJSRuntime _jsRuntime { get; set; }
	[Inject] private IDbContextFactory<SapDb> _sapDbContextFactory { get; set; }

	dynamic Breadcrumbs = new dynamic[]
		{
				"Forms and Reports",
				new dynamic []{
				$"Irradiation Label Printing",
				$"/IrradiationLabelPrinting"
				},
				"Sales Order Details"
		};

	IList<LotNo> selectedLotNos;

	RadzenDataGrid<LotNo> grid;

	IrradiationSalesOrderDetailsViewModel model = new IrradiationSalesOrderDetailsViewModel();



	protected override void OnInitialized()
	{
		try
		{
			model = _formsAndReportsService.InitilizeSalesOrderDetails(SONo);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task PrintIrradiationLabels()
	{
		if (selectedLotNos == null || selectedLotNos.Count <= 0)
		{
			_jsRuntime.InvokeVoidAsync("ShowResult", "Warning", "Select atleast one lot no to print.");
		}
		else
		{
			string header = "\"Code\", \"LotNo\", \"MinimumDose\", \"Date\", \"ItemSubGroup\"";
			string args = "";
			string filePath = $"{_environment.WebRootPath}/PRINT_LAYOUT/IrradiationBoxStickers.rpt";
			string database = $"{_environment.WebRootPath}/PRINT_LAYOUT/IrradiationDatabase.txt";

			using(var db = _sapDbContextFactory.CreateDbContext())
			foreach (LotNo bLabel in selectedLotNos)
			{
				var batchExists = db.OBTN.FirstOrDefault(x => x.DistNumber == bLabel.IrradiationLotNo) is not null;
				if (!batchExists) continue;
				args += $"nextLine\"{bLabel.IrradiationLotNo}-{model.SalesOrderDetail.MinDose}-{model.SalesOrderDetail.IrradiationDate.ToString("yyyy-MM-dd")}\", \"{bLabel.IrradiationLotNo}\", \"{model.SalesOrderDetail.MinDose}\", \"{model.SalesOrderDetail.IrradiationDate.ToString("yyyy-MM-dd")}\", \"{model.SalesOrderDetail.ItemGroup}\"";
			}

			try
			{
				//bool result = await _printingService.Print(header, args, selectedPrinter, filePath, database);
				//HttpResponseMessage result = await _printingService.Print(header, args, selectedPrinter, filePath, database);
				//if (result.IsSuccessStatusCode)
				if (await OpenReportInNewWindow(header, args, filePath.Replace("\\", "/"), database.Replace("\\", "/")))
				{
					//reportStream = await result.Content.ReadAsStreamAsync();

					_jsRuntime.InvokeVoidAsync("ShowResult", "Info", args);
					/*Console.WriteLine(args);*/

					_jsRuntime.InvokeVoidAsync("ShowResult", "Success", selectedLotNos != null ? selectedLotNos.Count : "0");

				}
				else
				{
					_jsRuntime.InvokeVoidAsync("ShowResult", "Error", "Something went wrong when printing. Please try again later.");

				}
			}
			catch (Exception e)
			{
				_jsRuntime.InvokeVoidAsync("ShowResult", "Error", e.Message);
				throw;
			}
		}
	}

	private async Task<bool> OpenReportInNewWindow(string Header, string args, string FilePath, string Database)
	{
		try
		{
            File.WriteAllText(Database, Header + args.Replace("nextLine", "\n"));

            string endpoint = _configuration["PrinterAPI"]?.ToString() ?? "";
            //string url = $"http://localhost:44308/api/Print?Header={Header}&args={args}&PrinterName={string.Empty}&FilePath={FilePath}&Database={Database}";
            //string url = $"{endpoint}/api/Print?Header={Header}&args={args}&PrinterName={string.Empty}&FilePath={FilePath}&Database={Database}";
            string url = $"{endpoint}/api/Print?FilePath={FilePath}";

            // JavaScript to open the report in a new window
            string script = "var newWindow = window.open(`" + url + "`, '_blank');";
			script += "newWindow.focus();";

			// Execute the JavaScript
			await _jsRuntime.InvokeVoidAsync("eval", script);

			return true;
		}
		catch (Exception)
		{

			throw;
		}

	}
}
