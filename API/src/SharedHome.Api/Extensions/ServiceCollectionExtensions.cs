using Microsoft.OpenApi.Models;
using SharedHome.Api.Swagger.Filters;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace SharedHome.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string ApiTitle = "HomeShared API";
        private const string ApiVersion = "v1";

        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            AddCors(services);

            AddSwagger(services);

            return services;
        }

        private static IServiceCollection AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "Origin",
                                  policy =>
                                  {
                                      policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200");
                                  });
            });

            return services;
        }

        private static IServiceCollection AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.CustomSchemaIds(x => x.FullName);
                options.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = ApiTitle,
                    Version = ApiVersion
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.ExampleFilters();

                options.SchemaFilter<SwaggerExcludeSchemaFilter>();
                options.OperationFilter<SwaggerExcludeOperationFilter>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<Program>();

            return services;
        }
    }
}
