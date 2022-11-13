using Microsoft.Extensions.DependencyInjection;

namespace SharedHome.Infrastructure.EF.Initializers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInitializer(this IServiceCollection services)
        {
            services.ConfigureOptions<InitializerOptionsSetup>();

            services.Scan(s => s.FromCallingAssembly()
                .AddClasses(c => c.AssignableTo(typeof(IDataInitializer)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        } 
    }
}
