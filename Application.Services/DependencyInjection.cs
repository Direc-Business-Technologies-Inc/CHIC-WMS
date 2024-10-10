namespace Application.Services;

public static class DependencyInjection
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddTransient<IAuthLoginService, AuthLoginService>();
		services.AddTransient<IAdministrativeService, AdministrativeService>();
		services.AddTransient<IBinServices, BinServices>();
		services.AddTransient<IQCMaintenanceService, QCMaintenanceService>();
		services.AddTransient<IScheduleService, ScheduleService>();
		services.AddTransient<IPrintingService, PrintingService>();
		services.AddTransient<IFormsAndReportsService, FormsAndReportsService>();
		services.AddTransient<IQCOrderService, QCOrderService>();
		services.AddTransient<IDashboardService, DashboardService>();
		services.AddTransient<ICertificateOfIrradiationService, CertificateOfIrradiationService>();

		//Automapper
		services.AddAutoMapper(typeof(AutoMapperRegisters));

		services.AddTransient<ISalesOrderService, SalesOrderService>();
		services.AddTransient<IReceivingService, ReceivingService>();
		services.AddTransient<IFacilityLocationService, FacilityLocationService>();
		services.AddTransient<IInventoryTransferService, InventoryTransferService>();
		services.AddTransient<IPalletService, PalletService>();
		services.AddTransient<IWarehouseService, WarehouseService>();
		services.AddTransient<IDispatchService, DispatchService>();
		services.AddTransient<IServiceTypeService, ServiceTypeService>();
		services.AddTransient<IBatchSerialService, BatchSerialService>();
		services.AddTransient<IDashboardNotificationService, DashboardNotificationService>();
		services.AddTransient<IConfigurationService, ConfigurationService>();

        return services;
	}
}
