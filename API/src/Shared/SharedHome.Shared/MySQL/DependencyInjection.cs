using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedHome.Shared.MySQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMySQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySQLSettings>(configuration.GetSection(MySQLSettings.SectionName));
            return services;
        }
    }
}
