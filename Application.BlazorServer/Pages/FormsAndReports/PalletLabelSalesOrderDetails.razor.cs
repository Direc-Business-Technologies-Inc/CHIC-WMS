using Application.Libraries.SAP.DB.Models;
using Azure.Core;
using System.Linq;
using static Application.Models.ViewModels.FormsAndReportsViewModel.PalletLabelSalesOrderDetailsViewModel;

namespace Application.BlazorServer.Pages.FormsAndReports;

public partial class PalletLabelSalesOrderDetails : ComponentBase
{
	[Parameter]
	public string SONo { get; set; } = "";

	[Inject] private IFormsAndReportsService _formsAndReportsService { get; set; }

	[Inject] private IConfiguration _configuration { get; set; }
	[Inject] private IWebHostEnvironment _environment { get; set; }
	[Inject] private IJSRuntime _jsRuntime { get; set; }

	dynamic Breadcrumbs = new dynamic[]
		{
				"Forms and Reports",
				new dynamic []{
				$"Pallet Label Printing",
				$"/PalletLabelPrinting"
				},
				"Sales Order Details"
		};

	IList<PalletLabelDetails> selectedPallets;

	List<string> BinStatustList;
	IEnumerable<string> SelectedBinStatuses;

	RadzenDataGrid<PalletLabelDetails> grid;
	IEnumerable<string> selectedBins;

	PalletLabelSalesOrderDetailsViewModel model = new PalletLabelSalesOrderDetailsViewModel();

	PalletLabelDetails palletRowData = new PalletLabelDetails();

	protected override void OnInitialized()
	{
		try
		{
			model = _formsAndReportsService.InitilizePalletLabelSalesOrderDetails(SONo);
			BinStatustList = model.BinLabelList.Select(x => x.BinStatus).Distinct().ToList();
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task PrintIrradiationLabels()
	{
		if (selectedPallets == null || selectedPallets.Count <= 0)
		{
			_jsRuntime.InvokeVoidAsync("ShowResult", "Warning", "Select atleast one pallet to print.");
		}
		else
		{
			string header = "\"Code\", \"BinLocation\", \"CustomerName\", \"ItemName\"";
			string args = "";
			string filePath = $"{_environment.WebRootPath}/PRINT_LAYOUT/PalletLabel.rpt";
			string database = $"{_environment.WebRootPath}/PRINT_LAYOUT/PalletLabelDatabase.txt";

			List<PalletLabelDetails> selectedPalletList = selectedPallets.OrderBy(x => x.PalletNo).ToList();
			foreach (PalletLabelDetails bLabel in selectedPalletList)
			{
				args += $"nextLine\"{bLabel.PalletNo}\", \"{bLabel.BinLocation}\", \"{model.SalesOrderDetail.CustomerName}\", \"{model.SalesOrderDetail.ItemName}\"";
			}

			try
			{
				if (await OpenReportInNewWindow(header, args, filePath.Replace("\\", "/"), database.Replace("\\", "/")))
				{

					//_jsRuntime.InvokeVoidAsync("ShowResult", "Info", args);

					//_jsRuntime.InvokeVoidAsync("ShowResult", "Success", selectedPallets != null ? selectedPallets.Count : "0");
					_jsRuntime.InvokeVoidAsync("ShowResult", "Success", "Printing success.");

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

	public void SelectSalesOrder(string value)
	{
		model = _formsAndReportsService.InitilizePalletLabelSalesOrderDetails(value);
		_jsRuntime.InvokeVoidAsync("HideModal");
	}
	private readonly string availableStatusStr = "Available";
    private readonly string occupiedStatusStr = "Occupied";
	public void SelectBin(string value)
	{
		if (palletRowData.BinLocation != "")
		{
			model.BinLabelList.Where(x => x.BinCode == palletRowData.BinLocation).FirstOrDefault().BinStatus = availableStatusStr;
		}

		palletRowData.BinLocation = value;
		model.BinLabelList.Where(x => x.BinCode == value).FirstOrDefault().BinStatus = occupiedStatusStr;

		var src = model.SalesOrderDetail.PalletLabelList;

		var idx = src.IndexOf(palletRowData);

		//var palletWithoutBin = src.Skip(idx).Where(x => string.IsNullOrWhiteSpace(x.BinLocation.Trim()) && x.IsReceived);
		var palletWithoutBin = src.Skip(idx + 1).Where(x => string.IsNullOrWhiteSpace(x.BinLocation.Trim()));

		// var availBins = model.BinLabelList.Where(x => string.IsNullOrWhiteSpace(x.BinStatus)).Take(palletWithoutBin.Count()).ToList();

		var binIdx = model.BinLabelList.FindIndex(x => x.BinCode == value);
		//     var availBins = model.BinLabelList.Skip(binIdx).Where(x=> x.BinStatus.Contains("Available"))
		//.OrderBy(x => x.WarehouseCode)
		//.ThenBy(x => x.Shelf)
		//.ThenBy(x => x.Level);
		var availBins = model.BinLabelList.Skip(binIdx).Where(x => x.BinStatus.Contains("Available"));
		var a = availBins.ToList();

		selectedPallets = new List<PalletLabelDetails>();


		using (var availBinsIterator = availBins.GetEnumerator())
		foreach (var pallet in palletWithoutBin)
		{
            if (!availBinsIterator.MoveNext()) break;
			var currBin = availBinsIterator.Current;
			if (currBin is null) break;
				pallet.BinLocation = currBin.BinCode;
				currBin.BinStatus = occupiedStatusStr;

				var bin = model.SalesOrderDetail.PalletLabelList.Where(x => x.BinLocation == currBin.BinCode).FirstOrDefault();
				if(bin != null)
					selectedPallets.Add(bin);
		}



		//model.BinLabelList.Where(x => x.BinCode == value).FirstOrDefault().BinStatus = "Occupied";
		//palletRowData.BinLocation = value;
		//model = _formsAndReportsService.InitilizePalletLabelSalesOrderDetails(value);
		_jsRuntime.InvokeVoidAsync("HideModal");
	}

	void FillPalletBins()
	{

	}

	void CellRender(DataGridCellRenderEventArgs<PalletLabelSalesOrderDetailsViewModel.BinLabelDetails> args)
	{
		if (args.Column.Title == "Bin Status")
		{
			args.Attributes.Add("style", $"background-color: {(args.Data.BinStatus == "Available" ? "var(--rz-success)" : "var(--rz-danger)")}; border-radius: 25px;");
		}
	}

	public async Task AssignBinLabel()
	{
		if (selectedPallets == null || selectedPallets.Count <= 0)
		{
			await _jsRuntime.InvokeVoidAsync("ShowResult", "Warning", "Select atleast one pallet to save.");
			return;
		}

		try
		{
			if (_formsAndReportsService.SavePalletLabel(selectedPallets.ToList(), model.SalesOrderDetail))
			{
				_jsRuntime.InvokeVoidAsync("ShowResult", "Success", "Pallets Assigned!");
				return;
			}

			_jsRuntime.InvokeVoidAsync("ShowResult", "Error", "Something went wrong when saving. Please try again later.");
		}
		catch (Exception ex)
		{
			_jsRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
			//throw;
		}
	}

	public async Task ClearBins()
	{
		var palletWithBins = model.SalesOrderDetail.PalletLabelList.Where(x => !string.IsNullOrWhiteSpace(x.BinLocation));

        var binsWithPallet = palletWithBins.Select(x => x.BinLocation).Distinct();

		var bins = model.BinLabelList.Where(x => binsWithPallet.Contains(x.BinCode));
        foreach(var bin in bins)
			bin.BinStatus = availableStatusStr;

		foreach (var pallet in palletWithBins)
			pallet.BinLocation = string.Empty;

		InvokeAsync(StateHasChanged);
    }

	void OnSelectedBinStatusChange(object value)
	{
		if (SelectedBinStatuses != null && !SelectedBinStatuses.Any())
		{
			SelectedBinStatuses = null;
		}
	}
}
