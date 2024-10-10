namespace DataManager.Services;

public static class DependencyInjection
{
	private static string ADDONDB_NAME = "CommonDb";
	#region Public IServiceCollection Extensions

	/// <summary>
	/// This creates its own Configuration so previous configuration will be overridden
	/// </summary>
	public static IServiceCollection AddDataServices(this IServiceCollection services)
	{
		var constr = GetCommonDbConnectionString(GetConfiguration());
		services.AddDataServices(constr);
		return services;
	}
	public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
	{
		var constr = GetCommonDbConnectionString(configuration);

		services.AddDataServices(constr);
		return services;
	}

	private static IServiceCollection AddDataServices(this IServiceCollection services, string constr)
	{
		services.AddAppSettings();

		RegisterServices(services);

		services.AddDbContextFactory<Context>(options =>
		options.UseSqlServer(constr));

		return services;
	}
	#endregion

	#region Add/Remove Services Here 
	/// <summary>
	/// Register services. Modify this when add/removing servies
	/// </summary>
	/// <param name="services"></param>
	private static void RegisterServices(IServiceCollection services)
	{
		services.AddScoped<IAuthDataService, AuthDataService>();
		services.AddScoped<IBinDataServices, BinDataServices>();
		services.AddScoped<IQCMaintenanceDataService, QCMaintenanceDataService>();
		services.AddScoped<IQCOrderDataService, QCOrderDataService>();
		services.AddScoped<ICertificatOfIrradiationDataService, CertificatOfIrradiationDataService>();
        services.AddScoped<IDashboardNotificationDataService, DashboardNotificationDataService>();
        services.AddScoped<IConfigurationDataService, ConfigurationDataService>();
    }
	#endregion
	/// <summary>
	/// Get the default configuration from appsettings.json (this doesn't automatically consider the environment appsetting).
	/// This is the first implementation.
	/// </summary>
	private static IConfigurationRoot GetConfiguration()
	{
		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json", optional: true)
			.Build();
		return configuration;
	}

	private static string GetCommonDbConnectionString(IConfiguration configuration)
	{
		var conString = configuration.GetConnectionString(ADDONDB_NAME);
		return conString;
	}
	/// <summary>
	///  Dunno why is this being registered. Just separating this function.
	/// </summary>
	private static IServiceCollection AddAppSettings(this IServiceCollection services)
	{
		var appSettings = new AppSettings();
		//bind the data in appsettings
		//configuration.GetSection(nameof(AppSettings));
		//.Bind(appSettings);
		services.AddSingleton(appSettings);

		return services;

	}
}
