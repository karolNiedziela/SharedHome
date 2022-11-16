using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Exceptions.Common;

namespace SharedHome.Shared.Exceptions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        {
            services.AddSingleton<ProblemDetailsFactory, SharedHomeProblemDetailsFactory>();
            services.AddScoped<IExceptionToErrorResponseMapper, ExceptionToErrorResponseMapper>();

            return services;
        }

        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
