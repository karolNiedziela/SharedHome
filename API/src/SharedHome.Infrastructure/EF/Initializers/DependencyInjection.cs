using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Options;

namespace SharedHome.Infrastructure.EF.Initializers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInitializer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<InitializerSettings>(configuration.GetSection(InitializerSettings.SectionName));

            services.Scan(s => s.FromCallingAssembly()
                .AddClasses(c => c.AssignableTo(typeof(IDataInitializer)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        } 
    }
}
