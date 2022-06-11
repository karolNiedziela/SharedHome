using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Shared.Exceptions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        {
            services.AddScoped<GlobalErrorHandlerMiddleware>();
            services.AddScoped<IExceptionToErrorResponseMapper, ExceptionToErrorResponseMapper>();

            return services;
        }

        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();

            return app;
        }
    }
}
