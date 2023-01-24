using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Identity.EF.Contexts;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Initializers;
using SharedHome.Infrastructure.EF.Interceptors;
using SharedHome.Infrastructure.EF.Options;
using SharedHome.Infrastructure.EF.Repositories;
using SharedHome.Notifications.Repositories;

namespace SharedHome.Infrastructure.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMySharedHomeSQL(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IHouseGroupRepository, HouseGroupRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            // Interceptors
            services.AddScoped<AuditableInterceptor>();
            services.AddSingleton<DomainEventToOutboxMessageInterceptor>();

            services.ConfigureOptions<MySQLOptionsSetup>();

            services.AddDbContext<WriteSharedHomeDbContext>((serviceProvider, options) =>
            {
                var mySQLOptions = serviceProvider.GetService<IOptions<MySQLOptions>>()!.Value;

                var auditableInterceptor = serviceProvider.GetRequiredService<AuditableInterceptor>();
                var domainEventToOutboxMessageInterceptor = serviceProvider.GetRequiredService<DomainEventToOutboxMessageInterceptor>();

                options.UseMySql(mySQLOptions.ConnectionString, ServerVersion.AutoDetect(mySQLOptions.ConnectionString), mySqlOptionsAction =>
                {
                    mySqlOptionsAction.EnableRetryOnFailure(mySQLOptions.MaxRetryCount);
                })
                .AddInterceptors(
                    auditableInterceptor, 
                    domainEventToOutboxMessageInterceptor);

                options.EnableDetailedErrors(mySQLOptions.EnableDetailedErrors);

                options.EnableSensitiveDataLogging(mySQLOptions.EnableSensitiveDataLogging);
            });

            services.AddDbContext<ReadSharedHomeDbContext>((servicePrvoider, options) =>
            {
                var mySQLOptions = servicePrvoider.GetService<IOptions<MySQLOptions>>()!.Value;

                options.UseMySql(mySQLOptions.ConnectionString, ServerVersion.AutoDetect(mySQLOptions.ConnectionString), mySqlOptionsAction =>
                {
                    mySqlOptionsAction.EnableRetryOnFailure(mySQLOptions.MaxRetryCount);
                });

                options.EnableDetailedErrors(mySQLOptions.EnableDetailedErrors);

                options.EnableSensitiveDataLogging(mySQLOptions.EnableSensitiveDataLogging);
            });

            services.AddDbContext<IdentitySharedHomeDbContext>((servicePrvoider, options) =>
            {
                var mySQLOptions = servicePrvoider.GetService<IOptions<MySQLOptions>>()!.Value;

                options.UseMySql(mySQLOptions.ConnectionString, ServerVersion.AutoDetect(mySQLOptions.ConnectionString), mySqlOptionsAction =>
                {
                    mySqlOptionsAction.EnableRetryOnFailure(mySQLOptions.MaxRetryCount);
                });

                options.EnableDetailedErrors(mySQLOptions.EnableDetailedErrors);

                options.EnableSensitiveDataLogging(mySQLOptions.EnableSensitiveDataLogging);
            });

            services.AddInitializer();
            services.AddHostedService<MigratorHostedService>();
            services.AddHostedService<SeedDataService>();

            return services;
        }
    }
}
