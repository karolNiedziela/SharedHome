using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedHome.Infrastructure.EF.Initializers;
using SharedHome.Infrastructure.EF.Initializers.Write;

namespace SharedHome.Infrastructure.EF
{
    public class SeedDataService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedDataService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var webApplication = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            if (webApplication.EnvironmentName == "Test")
            {
                return;
            }

            var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>().ToList();

            // PersonInitializer must be first
            await dataInitializers.First(dt => dt.GetType().Name == typeof(PersonInitializer).Name).SeedAsync();

            foreach (var dataInitializer in dataInitializers.Where(dt => dt.GetType().Name != typeof(PersonInitializer).Name))
            {
                await dataInitializer.SeedAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
