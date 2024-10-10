using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Application.Hubs
{
    public static class DependencyInjection
    {
        public static void MapApplicationHubs(this WebApplication app)
        {
            app.MapHub<DashboardNotifHub>("/hubs/dashboard-notification");
        }
    }
}
