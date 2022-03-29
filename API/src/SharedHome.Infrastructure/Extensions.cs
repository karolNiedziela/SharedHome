using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application;
using SharedHome.Infrastructure.EF;
using SharedHome.Infrastructure.Identity;

namespace SharedHome.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(new[] { typeof(ApplicationAssemblyReference).Assembly, typeof(InfrastructureAssemblyReference).Assembly });

            services.AddMySQL(configuration);
            services.AddIdentity();

            return services;
        }
    }
}
