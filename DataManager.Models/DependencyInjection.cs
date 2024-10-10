using DataManager.Models.Registers;
using Microsoft.Extensions.DependencyInjection;

namespace DataManager.Models;

public static class DependencyInjection
{
	public static IServiceCollection AddDataModels(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(AutoMapperRegisters));

        return services;
	}
}
