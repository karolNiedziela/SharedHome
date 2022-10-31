using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Notifications;
using System.Reflection;

namespace SharedHome.Infrastructure.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
