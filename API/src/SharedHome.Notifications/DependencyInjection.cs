using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Notifications.Hubs;
using SharedHome.Notifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Notifications
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddScoped<IAppNotificationService, AppNotificationService>();
            services.AddScoped<IAppNotificationInformationResolver, AppNotificationInformationResolver>();

            services.AddSignalR();

            return services;
        }

        public static IApplicationBuilder UseNotifications(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BroadcastHub>("/notify");
            });

            return applicationBuilder;
        }
    }
}
