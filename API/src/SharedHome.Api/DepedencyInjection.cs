﻿using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using SharedHome.Api.HealthChecks;
using SharedHome.Api.Logging;
using SharedHome.Api.Swagger.Filters;
using SharedHome.Shared.Constants;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace SharedHome.Api
{
    public static class DepedencyInjection
    {
        private const string ApiTitle = "HomeShared API";
        private const string ApiVersion = "v1";

        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers(
                options =>
                {
                    options.SuppressAsyncSuffixInActionNames = false;
                })
               .AddDataAnnotationsLocalization(options =>
               {
                   options.DataAnnotationLocalizerProvider = (type, factory) =>
                   {
                       return factory.Create(Resources.DataAnnotationMessage, Assembly.GetEntryAssembly()!.GetName().Name!);
                   };
               })
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.Converters.Add(new StringEnumConverter());
                   options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; 
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

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var languages = context.Request.Headers["Accept-Language"].ToString();
                    var currentLanguage = languages.Split(',').FirstOrDefault();
                    var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? "en-US" : currentLanguage;

                    if (!supportedCultures.Where(s => s.Name.Equals(defaultLanguage)).Any())
                    {
                        defaultLanguage = "en-US";
                    }

                    return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage))!;
                }));
            });

            services.ConfigureOptions<LoggingOptionsSetup>();

            AddCors(services);

            AddSwagger(services);

            services.AddAppHealthChecks();

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

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
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
