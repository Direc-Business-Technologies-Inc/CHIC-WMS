using System.Drawing.Printing;
using static Application.Models.ViewModels.BinLabelViewModel;

namespace Application.BlazorServer.Pages.FormsAndReports;

public partial class BinLabelPrinting : ComponentBase
{
	[Inject]
	private IJSRuntime _jsRuntime { get; set; }

	[Inject]
	private IBinServices _binServices { get; set; }

	[Inject]
	private IPrintingService _printingService { get; set; }

	[Inject]
	private IWebHostEnvironment _environment { get; set; }

    [Inject] 
	private IConfiguration _configuration { get; set; }

    string[] Breadcrumbs;

	BinLabel model = new BinLabel();

	List<BinLabel> binList = new List<BinLabel>();

	IList<BinLabel> selectedBinLabels;

	RadzenDataGrid<BinLabel> grid;

	List<WarehouseCode> whsCodes;

	List<PrinterNames> printerlist = new List<PrinterNames>();

	string warehouseCode = "";

	/*IEnumerable<PrinterNames> printers;*/

	PrinterSettings.StringCollection printers;
	string selectedPrinter = "";

	private Stream reportStream;

	protected override async Task OnInitializedAsync()
	{
		// Get the list of installed printers
		/*StringCollection temp = PrinterSettings.InstalledPrinters;
            IEnumerable<PrinterNames> printerNamess = temp.Cast<PrinterNames>();*/

		printers = PrinterSettings.InstalledPrinters;

		foreach (string printer in printers)
		{
			printerlist.Add(new PrinterNames { printerName = printer });
		}

		//Breadcrumbs = new string[] { "Bins", "Bin Label Printing" };
		Breadcrumbs = new string[] { "Forms and Reports", "Bin Label Printing" };
		selectedPrinter = printerlist.FirstOrDefault().printerName;

		try
		{
			whsCodes = _binServices.GetWarehousesCodes();
		}
		catch (Exception)
		{

			throw;
		}
	}

	public void SelectInput(string fromSelect, string value)
	{
		if (fromSelect == "Warehouse Code")
		{
			warehouseCode = value;
			binList = _binServices.GetBinsData(warehouseCode);
		}
		else
		{
			selectedPrinter = value;
		}
		_jsRuntime.InvokeVoidAsync("HideModal");
	}

	public async Task PrintBinLabels()
	{
		if (warehouseCode == "")
		{
			_jsRuntime.InvokeVoidAsync("ShowResult", "Warning", "Select a warehouse and atleast one bin to print.");
		}
		else if (selectedBinLabels == null || selectedBinLabels.Count <= 0)
		{
			_jsRuntime.InvokeVoidAsync("ShowResult", "Warning", "Select atleast one bin label to print.");
		}
		else if (selectedPrinter == "" || selectedPrinter == null)
		{
			_jsRuntime.InvokeVoidAsync("ShowResult", "Warning", "No printer selected.");
		}
		else
		{
			string header = "\"BinCodeInner\", \"BinCodeOuter\", \"BinHeader\", \"Shelf\", \"Row\", \"Level\"";
			string args = "";
			string filePath = $"{_environment.WebRootPath}/PRINT_LAYOUT/BinLabel.rpt";
			string database = $"{_environment.WebRootPath}/PRINT_LAYOUT/Database.txt";

			foreach (BinLabel bLabel in selectedBinLabels)
			{
				args += $"nextLine\"{bLabel.WarehouseCode}-{bLabel.Shelf}-{bLabel.Row}-{bLabel.Level}-I\", \"{bLabel.WarehouseCode}-{bLabel.Shelf}-{bLabel.Row}-{bLabel.Level}-O\", \"{bLabel.WarehouseCode}-{bLabel.Shelf}-{bLabel.Row}-{bLabel.Level}\", \"{bLabel.Shelf}\", \"{bLabel.Row}\", \"{bLabel.Level}\"";
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

					_jsRuntime.InvokeVoidAsync("ShowResult", "Success", selectedBinLabels != null ? selectedBinLabels.Count : "0");

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
