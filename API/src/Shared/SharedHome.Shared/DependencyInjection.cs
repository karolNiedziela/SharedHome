using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Abstractions.User;
using SharedHome.Shared.Email;
using SharedHome.Shared.Exceptions;
using SharedHome.Shared.MySQL;
using SharedHome.Shared.Time;
using SharedHome.Shared.User;

namespace SharedHome.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEmail(configuration);
            services.AddMySQL(configuration);

            services.AddErrorHandling();

            services.AddSingleton<ITimeProvider, UtcTimeProvider>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddEndpointsApiExplorer();      

            services.Configure<GeneralSettings>(configuration.GetSection(GeneralSettings.SectionName));

            return services;
        }

        public static IApplicationBuilder UseShared(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseErrorHandling();

            return applicationBuilder;
        }
    }
}
