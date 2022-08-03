using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Identity.Entities;
using SharedHome.Identity.EF.Contexts;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Authentication;
using SharedHome.Identity.EF;

namespace SharedHome.Identity
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddSharedHomeIdentity(this IServiceCollection services, IConfiguration configuration)
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
            .AddErrorDescriber<MultilanguageIdentityErrorDescriber>()
            .AddEntityFrameworkStores<IdentitySharedHomeDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuth(configuration);

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddIdentitySQL(configuration);

            return services;
        }
    }
}
