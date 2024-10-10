namespace Application.BlazorServer.Pages.Administration.Configurations;

public partial class Configurations : ComponentBase
{
	[Inject] IConfigurationService _configurationService {  get; set; }
	ConfigurationViewModel model = new ConfigurationViewModel();

	dynamic Breadcrumbs = new dynamic[]
	{
		"Configurations"
	};

	protected override void OnInitialized()
	{
		model = _configurationService.InitializeConfigurations();
	}
}