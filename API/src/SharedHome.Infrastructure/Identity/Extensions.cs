using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application.Identity;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Services;

namespace SharedHome.Infrastructure.Identity
{
    public static class Extensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // TODO: Provider stronger password requirement
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<IdentitySharedHomeDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();


            return services;
        }
    }
}
