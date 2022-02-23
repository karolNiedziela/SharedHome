using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Options;
using SharedHome.Infrastructure.EF.Repositories;
using SharedHome.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF
{
    public static class Extensions
    {
        public static IServiceCollection AddMySQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IHouseGroupRepository, HouseGroupRepository>();

            var mySQLOptions = configuration.GetOptions<MySQLOptions>(MySQLOptions.SQLOptionsName);

            services.AddDbContext<SharedHomeDbContext>(options =>
            {
                options.UseMySql(mySQLOptions.ConnectionString, ServerVersion.AutoDetect(mySQLOptions.ConnectionString));
            });

            services.AddHostedService<MigratorHostedService>();

            return services;
        }
    }
}
