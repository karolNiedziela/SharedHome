using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application.Bills.Services;
using SharedHome.Application.Common.Events;
using SharedHome.Application.Notifications;
using SharedHome.Application.Notifications.Hubs;
using SharedHome.Application.Notifications.Services;
using SharedHome.Application.PipelineBehaviours;
using SharedHome.Application.ShoppingLists.Services;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Common.Events;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Notifications.Services;
using SharedHome.Notifications.Validators;
using SharedHome.Shared.Settings;

namespace SharedHome.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserInformationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PagedQueryBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddScoped<IShoppingListService, ShoppingListService>();
            services.AddScoped<IBillService, BillService>();

            services.AddScoped<IAppNotificationService, AppNotificationService>();
            services.AddScoped<IAppNotificationInformationResolver, AppNotificationInformationResolver>();

            services.AddScoped<IAppNotificationFieldValidator, NameFieldValidator>();
            services.AddScoped<IAppNotificationFieldValidator, OperationTypeFieldValidator>();
            services.AddScoped<IAppNotificationFieldValidator, TargetTypeFieldValidator>();

            services.AddSignalR();
            services.Configure<SignalRSettings>(configuration.GetSection(SignalRSettings.SectionName));

            return services;
        }

        public static IApplicationBuilder UseNotifications(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            var signalRSettings = configuration.GetSettings<SignalRSettings>(SignalRSettings.SectionName);

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<HouseGroupNotificationHub>(signalRSettings.NotificationsPattern);
            });

            return applicationBuilder;
        }
    }
}
