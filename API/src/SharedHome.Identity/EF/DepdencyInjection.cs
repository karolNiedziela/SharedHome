using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Identity.EF.Contexts;
using SharedHome.Shared.MySQL;
using SharedHome.Shared.Settings;

namespace SharedHome.Identity.EF
{
    public static class DepdencyInjection
    {
        public static IServiceCollection AddIdentitySQL(this IServiceCollection services, IConfiguration configuration)
        {
            var mySQLOptions = configuration.GetSettings<MySQLSettings>(MySQLSettings.SectionName);

            services.AddDbContext<IdentitySharedHomeDbContext>(options =>
            {
                options.UseMySql(mySQLOptions.ConnectionString, ServerVersion.AutoDetect(mySQLOptions.ConnectionString));
            });

            return services;
        }
    }
}
