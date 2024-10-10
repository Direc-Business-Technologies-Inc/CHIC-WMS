namespace Application.BlazorServer.Pages.FormsAndReports;

public partial class PalletLabelPrinting : ComponentBase
{
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] private NavigationManager _navManager { get; set; }
	[Inject] private IFormsAndReportsService _formsAndReportsService { get; set; }
	private IJSObjectReference _js { get; set; } = null;

	dynamic Breadcrumbs = new dynamic[]

	{
		"Forms and Reports",
		"Pallet Label Printing"
	};

	PalletLabelViewModel model = new PalletLabelViewModel();

	string searchValue = "";

	List<PalletLabelViewModel.PalletLabel> tableData = new List<PalletLabelViewModel.PalletLabel>();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/PalletLabelPrinting.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("initializeDateRange", DotNetObjectReference.Create(this));
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
		}
	}

	protected override void OnInitialized()
	{
		try
		{
			model = _formsAndReportsService.InitializePalletLabelPrinting();
			tableData = model.PalletLabelList;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task SelectSchedule(string SONo)
	{
		_navManager.NavigateTo($"PalletLabelSalesOrderDetails/{SONo}");
	}

	[JSInvokable("FilterPalletLabel")]
	public async Task FilterPalletLabel(string start, string end)
	{
		_formsAndReportsService.FilterPalletLabel(model, start, end);

		if(searchValue != "")
		{
			tableData = model.PalletLabelList.Where(x => x.SONo.ToLower().Contains(searchValue.ToLower()) 
			|| x.CustomerName.ToLower().Contains(searchValue.ToLower())
			|| x.ItemName.ToLower().Contains(searchValue.ToLower())).ToList();
		}
		else
		{
			tableData = model.PalletLabelList;
		}

		StateHasChanged();
	}

	public async Task SearchPalletLabel(string value)
	{
		if (value != "")
		{
			tableData = model.PalletLabelList.Where(x => x.SONo.ToLower().Contains(value.ToLower()) 
			|| x.CustomerName.ToLower().Contains(value.ToLower()) 
			|| x.ItemName.ToLower().Contains(value.ToLower())).ToList();
		}
		else
		{
			tableData = model.PalletLabelList;
		}

	}
}
