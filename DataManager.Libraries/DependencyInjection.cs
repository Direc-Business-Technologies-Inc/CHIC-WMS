using DataManager.Libraries.DataAccess;
using DataManager.Libraries.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DataManager.Libraries;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLibraries(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        configuration.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);
        services.AddSingleton<IMySqlDataAccess, MySqlDataAccess>();

        return services;
    }
}
