using DataManager.Libraries;
using DataManager.Services;
using DataManager.Models;
using System.Reflection;

namespace DataManager.ApiServer.Registers;

public static class WebApplicationService
{
	public static void ConfigureServices(this WebApplicationBuilder builder)
	{
		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1",
				new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "Direc API",
					Description = "Direc API Documentation",
					Version = "v1"
				});
			options.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
			{
				Url = "https://localhost:7045"
			});

			var fileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
			var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
			options.IncludeXmlComments(filePath);
		});

		builder.Services
			.AddDataLibraries()
			.AddDataServices()
			.AddDataModels();

		builder.Host.UseSerilog((context, configuration) =>
		configuration.ReadFrom.Configuration(context.Configuration));

		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json", optional: true)
			.Build();
	}
}
