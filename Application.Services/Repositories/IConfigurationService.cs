namespace Application.Services.Repositories
{
    public interface IConfigurationService
    {
        ConfigurationViewModel InitializeConfigurations();

		#region QCMaintenance Config
		ConfigurationViewModel InitializeQCMaintenanceConfig();
        bool SaveQCMaintenanceConfig(List<ConfigurationViewModel.QCMaintenanceItem> model);
		#endregion
	}
}