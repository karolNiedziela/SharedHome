using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Email;
using SharedHome.Shared.Exceptions;
using SharedHome.Shared.Time;
using SharedHome.Shared.User;

namespace SharedHome.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddEmail();

            services.AddErrorHandling();

            services.AddSingleton<ITimeProvider, UtcTimeProvider>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddEndpointsApiExplorer();      

            services.ConfigureOptions<GeneralOptionsSetup>();

            return services;
        }

        public static IApplicationBuilder UseShared(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseErrorHandling();

            return applicationBuilder;
        }
    }
}
