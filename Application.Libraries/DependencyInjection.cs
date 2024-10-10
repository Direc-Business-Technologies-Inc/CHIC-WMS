using Application.Libraries.DataAccess;
using Application.Libraries.Repositories;
using Application.Libraries.SAP;
using Application.Libraries.SAP.DB;
using Application.Libraries.SAP.DB.Models;
using Application.Libraries.Utilies;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Libraries;

public static class DependencyInjection
{
    public static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        configuration.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddSingleton<IMsSqlDataAccess, MsSqlDataAccess>();
        services.AddSingleton<IServiceLayerDataAccess, ServiceLayerDataAccess>();
        services.AddSingleton<IUtilities, Utilities>();

        //services.AddDbContext<SapDb>(o => o.UseSqlServer("name=SAP"));
        services.AddDbContextFactory<SapDb>(
        options =>
            options.UseSqlServer("name=SAP")
        );

        services.AddAutoMapper(
            typeof(Mappers.DocumentMapper),
            typeof(Mappers.ServiceLayerEnumMapper),
            typeof(Mappers.WarehouseMapper),
            typeof(Mappers.ServiceTypeMapper)
            );

        return services;
    }
}
