using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Abstractions.User;
using SharedHome.Shared.Email;
using SharedHome.Shared.Exceptions;
using SharedHome.Shared.Filters;
using SharedHome.Shared.Time;
using SharedHome.Shared.User;

namespace SharedHome.Shared
{
    public static class DependencyInjection
    {
        private const string ApiTitle = "HomeShared API";
        private const string ApiVersion = "v1";

        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEmail(configuration);

            services.AddErrorHandling();

            services.AddSingleton<ITimeProvider, UtcTimeProvider>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.CustomSchemaIds(x => x.FullName);
                swagger.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = ApiTitle,
                    Version = ApiVersion
                });

                swagger.OperationFilter<SwaggerExcludeFilter>();
            });

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
