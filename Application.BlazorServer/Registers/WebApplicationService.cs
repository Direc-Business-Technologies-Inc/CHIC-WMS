using Application.BlazorServer.Security;
using Application.Libraries;
using Application.Libraries.SAP.SL;
using Application.Services;
using BlazorStrap;
using DataManager.Libraries;
using DataManager.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;

namespace Application.BlazorServer.Registers;

public static class WebApplicationService
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddBlazorStrap();

        builder.Logging.SetMinimumLevel(LogLevel.Debug);
        builder.Logging.AddConsole();

		builder.Services.AddSignalR(e => {
			e.MaximumReceiveMessageSize = 102400000;
		});
        builder.Services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
               new[] { "application/octet-stream" });
        });

		//builder.Services.AddScoped<ContextMenuService>();

		builder.Services.AddScoped<AuthenticationStateProvider,
	        CustomAuthenticationStateProvider>();

		builder.Services.AddSingleton<AuthenticationService>();

		builder.Services
            .AddDataLibraries()
            .AddDataServices(builder.Configuration);

        builder.Services
            .AddLibraries()
            .AddServices();        
    }
}
