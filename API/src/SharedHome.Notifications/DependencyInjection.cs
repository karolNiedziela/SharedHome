using Microsoft.Extensions.DependencyInjection;
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

            return services;
        }
    }
}
