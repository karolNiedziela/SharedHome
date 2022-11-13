using SharedHome.Api.HealthChecks.Custom;
using SharedHome.Identity.EF.Contexts;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Api.HealthChecks
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddHealthChecks()
                .AddDbContextCheck<WriteSharedHomeDbContext>("WriteContext")
                .AddDbContextCheck<IdentitySharedHomeDbContext>("IdentityContext")
                .AddCheck<SignalRHealthCheck>("SignalR");

            return services;
        }
    }
}
