namespace DataManager.Services.Repositories
{
    public interface IConfigurationDataService
    {
        Configurations GetConfiguration(string Code);
        List<Configurations> GetConfigurations();
        List<ConfigurationItems> SaveConfigurationItems(string Code, List<ConfigurationItems> Items);
        List<ConfigurationItems> GetConfigurationItems(string Code);

	}
}