using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Infrastructure.Authentication;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Services;

namespace SharedHome.Infrastructure.Identity
{
    public static class Extensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
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

            return services;
        }
    }
}
