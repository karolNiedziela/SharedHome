using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF
{
    public class MigratorHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigratorHostedService(IServiceProvider serviceProvider)
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

            var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteSharedHomeDbContext>();

            await writeDbContext.Database.MigrateAsync();

            var readDbContext = scope.ServiceProvider.GetRequiredService<ReadSharedHomeDbContext>();

            await readDbContext.Database.MigrateAsync();

            var identityDbContext = scope.ServiceProvider.GetRequiredService<IdentitySharedHomeDbContext>();

            await identityDbContext.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
