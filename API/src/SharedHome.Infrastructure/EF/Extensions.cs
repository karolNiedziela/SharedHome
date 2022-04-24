using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application.Services;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Options;
using SharedHome.Infrastructure.EF.Repositories;
using SharedHome.Infrastructure.EF.Services;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Options;
using System.Linq;

namespace SharedHome.Infrastructure.EF
{
    public static class Extensions
    {
        public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int items)
        {
            var totalItems = await data.CountAsync();
            var totalPages = totalItems <= items ? 1 : (int)Math.Floor((double)totalItems / items);
            var result = await data.Skip((page - 1) * items).Take(items).ToListAsync();

            return new Paged<T>(result, page, items, totalPages, totalItems);
        }

        public static IServiceCollection AddMySQL(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IHouseGroupRepository, HouseGroupRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IBillRepository, BillRepository>();


            //Services
            services.AddScoped<IInvitationService, InvitationService>();
            services.AddScoped<IHouseGroupService, HouseGroupService>();

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
