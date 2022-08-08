using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application;
using SharedHome.Infrastructure.EF;
using SharedHome.Infrastructure.Mapping;

namespace SharedHome.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(new[] { typeof(ApplicationAssemblyReference).Assembly, typeof(InfrastructureAssemblyReference).Assembly });

            services.AddMappings();

            services.AddMySharedHomeSQL(configuration);

            return services;
        }
    }
}
