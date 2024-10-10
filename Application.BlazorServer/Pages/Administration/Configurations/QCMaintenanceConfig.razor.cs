namespace Application.BlazorServer.Pages.Administration.Configurations;

public partial class QCMaintenanceConfig : ComponentBase
{
	[Inject] protected IConfigurationService _configurationService { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	string Values = "QA/QC Receiving Inspection Plan";
	private ushort newRowsCount { get; set; } = 1;
	IEnumerable<PlanType> PlanTypes;
	ConfigurationViewModel model = new ConfigurationViewModel();
	List<ConfigurationViewModel.QCMaintenanceItem> mainData = new List<ConfigurationViewModel.QCMaintenanceItem>();
	List<ConfigurationViewModel.QCMaintenanceItem> tableData = new List<ConfigurationViewModel.QCMaintenanceItem>();
	RadzenDataGrid<ConfigurationViewModel.QCMaintenanceItem>? parameterList = new RadzenDataGrid<ConfigurationViewModel.QCMaintenanceItem>();
	private bool isLoading = false;
	private class PlanType
	{
		public string PlanName { get; set; } = string.Empty;
	}

	protected override async void OnInitialized()
	{
		await ReloadData();
	}

	private async Task ReloadData()
	{
		model = _configurationService.InitializeQCMaintenanceConfig();

		mainData = model.QCMaintenanceItems;

		tableData = mainData;

		PlanTypes = new List<PlanType> {
				new PlanType
				{
					PlanName = "QA/QC Receiving Inspection Plan"
				},
				new PlanType
				{
					PlanName = "Dosimetry Quality Control Order"
				},
				new PlanType
				{
					PlanName = "Others"
				}
		};

		StateHasChanged();
	}

	public async Task AddParameterRow()
	{
		isLoading = true;

		for (ushort i = 0; i < newRowsCount; i++)
		{
			try
			{
				var item = new ConfigurationViewModel.QCMaintenanceItem();
				item.PlanType = Values;
				item.ParameterType = "Quantitative";
				item.Parameter = "";
				item.UoM = "N/A";
				item.SpecificationType = "Item Specifications";

				mainData.Add(item);

				await FilterPlanType();

			}
			catch (Exception)
			{
				isLoading = false;
			}
		}

		isLoading = false;
	}

	public async Task DeleteRow(ConfigurationViewModel.QCMaintenanceItem data)
	{
		mainData.Remove(data);

		await FilterPlanType();
	}

	public async Task ChangeParameterType(ChangeEventArgs args, ConfigurationViewModel.QCMaintenanceItem data)
	{
		data.ParameterType = args.Value.ToString();
	}

	public async Task FilterPlanType()
	{

		tableData = new List<ConfigurationViewModel.QCMaintenanceItem>();
		tableData = mainData.Where(x => x.PlanType == Values).ToList();

		await parameterList.Reload();
	}

	public async Task Save()
	{
		try
		{
			if(mainData.Where(x => string.IsNullOrEmpty(x.Parameter)).Any())
			{
				//Error Message
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Please fill up all parameters.");
				return;
			}

			if (_configurationService.SaveQCMaintenanceConfig(mainData))
			{
				//Success Message
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
				await ReloadData();
				await FilterPlanType();
			}
			else
			{
				//Error Message
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
			}
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
		}
	}
}