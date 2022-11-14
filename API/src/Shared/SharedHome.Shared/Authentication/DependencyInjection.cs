using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedHome.Shared.Abstractions.Authentication;
using System.Text;

namespace SharedHome.Shared.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtOptionsSetup>();

            var serviceProvider = services.BuildServiceProvider();
            var jwtOptions = serviceProvider.GetService<IOptions<JwtOptions>>()!.Value;

            services.AddSingleton<IAuthManager, AuthManager>();

            var key = Encoding.UTF8.GetBytes(jwtOptions.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = false,
                    ValidAudience = jwtOptions.Audience,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/notify")))
                        {
                            context.Token = accessToken;
                        }                        

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
