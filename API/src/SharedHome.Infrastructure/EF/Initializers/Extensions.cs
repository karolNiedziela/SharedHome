using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Options;

namespace SharedHome.Infrastructure.EF.Initializers
{
    public static class Extensions
    {
        public static IServiceCollection AddInitializer(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration.GetOptions<InitializerOptions>(InitializerOptions.InitializerOptionsName);
            services.AddSingleton(authOptions);

            services.Scan(s => s.FromCallingAssembly()
                .AddClasses(c => c.AssignableTo(typeof(IDataInitializer)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        } 
    }
}
