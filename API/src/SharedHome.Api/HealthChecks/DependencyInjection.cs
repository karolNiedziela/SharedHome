using SharedHome.Api.HealthChecks.Custom;
using SharedHome.Identity.EF.Contexts;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Options;
using SharedHome.Shared.Settings;

namespace SharedHome.Api.HealthChecks
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();

            var mySQLOptions = configuration.GetSettings<MySQLSettings>(MySQLSettings.SectionName);

            services.AddHealthChecks()
                .AddDbContextCheck<WriteSharedHomeDbContext>("WriteContext")
                .AddDbContextCheck<IdentitySharedHomeDbContext>("IdentityContext")
                .AddCheck<SignalRHealthCheck>("SignalR");

            return services;
        }
    }
}
