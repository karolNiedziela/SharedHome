using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Initializers;
using SharedHome.Infrastructure.EF.Options;
using SharedHome.Infrastructure.EF.ReadServices;
using SharedHome.Infrastructure.EF.Repositories;
using SharedHome.Shared.Options;

namespace SharedHome.Infrastructure.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMySQL(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IHouseGroupRepository, HouseGroupRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            //Services
            services.AddScoped<IInvitationReadService, InvitationReadService>();
            services.AddScoped<IHouseGroupReadService, HouseGroupService>();

            var mySQLOptions = configuration.GetOptions<MySQLOptions>(MySQLOptions.SQLOptionsName);

            services.AddDbContext<WriteSharedHomeDbContext>(options =>
            {
                options.UseMySql(mySQLOptions.ConnectionString, ServerVersion.AutoDetect(mySQLOptions.ConnectionString));
            });

            services.AddDbContext<ReadSharedHomeDbContext>(options =>
            {
                options.UseMySql(mySQLOptions.ConnectionString, ServerVersion.AutoDetect(mySQLOptions.ConnectionString));
            });

            services.AddDbContext<IdentitySharedHomeDbContext>(options =>
            {
                options.UseMySql(mySQLOptions.ConnectionString, ServerVersion.AutoDetect(mySQLOptions.ConnectionString));
            });


            services.AddInitializer(configuration);
            services.AddHostedService<MigratorHostedService>();
            services.AddHostedService<SeedDataService>();

            return services;
        }
    }
}
