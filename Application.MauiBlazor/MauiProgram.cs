using Application.MauiBlazor.Data;
using BlazorStrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

using Microsoft.AspNetCore.Components.WebView.Maui;
using Newtonsoft.Json;
using Application.Libraries.Utilies.Newtonsoft;
using Application.MauiBlazor.Services;
using Application.MauiBlazor.Hubs;
using Application.MauiBlazor.Hubs.Repositories;


#if ANDROID
using Application.MauiBlazor.Platforms.Android;
#endif

namespace Application.MauiBlazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                //ContractResolver = cr,
                Converters = new JsonConverter[]
                {
                    new SapConverters()
                }
            };
            #if ANDROID && DEBUG
                    Platforms.Android.DangerousTrustProvider.Register();
            #endif

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                   handlers.AddHandler<BlazorWebView, MauiBlazorWebViewHandler>();
#endif
                });

            LoadAppSetting(builder);
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazorStrap();

            builder.Services.AddAutoMapper(
                typeof(Models.Registers.AutoMapperRegisters), 
                typeof(Libraries.Mappers.ServiceLayerEnumMapper),
                typeof(Libraries.Mappers.DocumentMapper),
                typeof(Libraries.Registers.AutoMapperRegisters)
            );
            
            DataManager.Models.DependencyInjection.AddDataModels(builder.Services);

            DataManager.Libraries.DependencyInjection.AddDataLibraries(builder.Services);
            //DataManager.Services.DependencyInjection.AddDataServices(builder.Services);
            //Application.Services.DependencyInjection.AddServices(builder.Services);

            Application.Libraries.DependencyInjection.AddLibraries(builder.Services);

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(new HttpClient { });
            builder.Services.AddSingleton<RestServiceFactory>();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<Radzen.DialogService>();
            return builder.Build();
        }

        private static void LoadAppSetting(MauiAppBuilder builder)
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Application.MauiBlazor.appsettings.json");

            var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

            builder.Configuration.AddConfiguration(config);
        }
    }
}