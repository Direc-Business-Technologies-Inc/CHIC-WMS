namespace Application.BlazorServer.Pages.FormsAndReports;

public partial class IrradiationLabelPrinting : ComponentBase
{
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] private NavigationManager _navManager { get; set; }
	[Inject] private IFormsAndReportsService _formsAndReportsService { get; set; }
	private IJSObjectReference _js { get; set; } = default!;

	dynamic Breadcrumbs = new dynamic[]

	{
		"Forms and Reports",
		"Irradiation Label Printing"
	};

	IrradiationLabelPrintingViewModel model = new IrradiationLabelPrintingViewModel();

	string searchValue = "";

	List<IrradiationLabelPrintingViewModel.IrradiationLabel> tableData = new List<IrradiationLabelPrintingViewModel.IrradiationLabel>();
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/IrradiationLabelPrinting.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("initializeDateRange", DotNetObjectReference.Create(this));
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
		}
	}

	protected override void OnInitialized()
	{
		try
		{
			model = _formsAndReportsService.InitializeIrradiationLabelPrinting();
			tableData = model.IrradiationLabelList;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task SelectSchedule(string SONo)
	{
		_navManager.NavigateTo($"IrradiationSalesOrderDetails/{SONo}");
	}

	[JSInvokable("FilterIrradLabel")]
	public async Task FilterIrradLabel(string start, string end)
	{
        _formsAndReportsService.FilterIrradiationSchedule(model, start, end);

		if (searchValue != "")
		{
			tableData = model.IrradiationLabelList.Where(x => x.SONo.ToLower().Contains(searchValue.ToLower())
			|| x.CustomerName.ToLower().Contains(searchValue.ToLower())
			|| x.ItemName.ToLower().Contains(searchValue.ToLower())).ToList();
		}
		else
		{
			tableData = model.IrradiationLabelList;
		}

		StateHasChanged();
	}

	public async Task SearchIrradLabel(string value)
	{
		if (value != "")
		{
			tableData = model.IrradiationLabelList.Where(x => x.SONo.ToLower().Contains(value.ToLower())
			|| x.CustomerName.ToLower().Contains(value.ToLower())
			|| x.ItemName.ToLower().Contains(value.ToLower())).ToList();
		}
		else
		{
			tableData = model.IrradiationLabelList;
		}

	}
}
