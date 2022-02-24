using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Infrastructure.EF;
using SharedHome.Infrastructure.Identity;

namespace SharedHome.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMySQL(configuration);
            services.AddIdentity();

            return services;
        }
    }
}
