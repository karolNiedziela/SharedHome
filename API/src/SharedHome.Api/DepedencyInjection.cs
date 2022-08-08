﻿using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using SharedHome.Api.Swagger.Filters;
using SharedHome.Shared.Constants;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SharedHome.Api
{
    public static class DepedencyInjection
    {
        private const string ApiTitle = "HomeShared API";
        private const string ApiVersion = "v1";

        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
               })
               .AddDataAnnotationsLocalization(options =>
               {
                   options.DataAnnotationLocalizerProvider = (type, factory) =>
                   {
                       return factory.Create(Resources.DataAnnotationMessage, Assembly.GetEntryAssembly()!.GetName().Name!);
                   };
               });

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pl-PL")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            AddCors(services);

            AddSwagger(services);

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

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