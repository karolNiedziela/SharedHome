using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedHome.Application.Bills.Services;
using SharedHome.Application.Common.Events;
using SharedHome.Application.Common.User;
using SharedHome.Application.Notifications.Hubs;
using SharedHome.Application.Notifications.Options;
using SharedHome.Application.Notifications.Services;
using SharedHome.Application.PipelineBehaviours;
using SharedHome.Application.ShoppingLists.Services;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Common.Events;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Notifications.Services;
using SharedHome.Notifications.Validators;
using System;

namespace SharedHome.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
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

            var scope = services.BuildServiceProvider().CreateScope();

            var webApplication = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            if (webApplication.IsProduction())
            {
                services.AddSignalR()
                        .AddAzureSignalR();
            }

            services.ConfigureOptions<SignalROptionsSetup>();

            services.AddScoped<ICurrentUser, CurrentUser>();

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
