using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Identity.Authentication;
using SharedHome.Identity.EF.Contexts;
using SharedHome.Identity.Entities;
using SharedHome.Infrastructure.Identity.Services;

namespace SharedHome.Identity
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddSharedHomeIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
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

            services.AddAuth();

            services.AddScoped<IApplicationUserService, ApplicationUserService>();

            return services;
        }
    }
}
