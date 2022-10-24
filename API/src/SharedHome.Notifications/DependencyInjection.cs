using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Notifications.Hubs;
using SharedHome.Notifications.Services;
using SharedHome.Notifications.Validators;

namespace SharedHome.Notifications
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddScoped<IAppNotificationService, AppNotificationService>();
            services.AddScoped<IAppNotificationInformationResolver, AppNotificationInformationResolver>();
            services.AddScoped<IAppNotificationFieldValidator, NameFieldValidator>();
            services.AddScoped<IAppNotificationFieldValidator, OperationTypeFieldValidator>();
            services.AddScoped<IAppNotificationFieldValidator, TargetTypeFieldValidator>();

            services.AddSignalR();

            return services;
        }

        public static IApplicationBuilder UseNotifications(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<HouseGroupNotificationHub>("/notify");
            });

            return applicationBuilder;
        }
    }
}
