using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared
{
    public static class Extensions
    {
        private const string ApiTitle = "HomeShared API";
        private const string ApiVersion = "v1";

        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITime, UtcTime>();

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
            });

            return services;
        }

        public static IApplicationBuilder UseShared(this IApplicationBuilder applicationBuilder)
        {

            return applicationBuilder;
        }
    }
}
