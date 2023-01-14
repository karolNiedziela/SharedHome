using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using Respawn;
using Respawn.Graph;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Interceptors;
using SharedHome.Infrastructure.EF.Options;
using SharedHome.IntegrationTests.DataSeeds;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests
{
    public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MySqlTestcontainer _container
            = new TestcontainersBuilder<MySqlTestcontainer>()
            .WithDatabase(new MySqlTestcontainerConfiguration
            {
                Database = "SharedHomeTests",
                Username = "root",
                Password = "IntegrationTests",
                Port = 3309,
            })
            .Build();

        private DbConnection _dbConnection = default!;

        private Respawner _respawner = default!;

        public IConfigurationRoot Configuration { get; private set; } = default!;

        public HttpClient HttpClient { get; private set; } = default!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json", true)
                .AddEnvironmentVariables()
                .Build();

                config.AddConfiguration(Configuration);
            });

            builder.UseEnvironment("Test");

            builder.ConfigureTestServices(services =>
            {              
                var writeDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<WriteSharedHomeDbContext>));
                
                if (writeDescriptor is not null)
                {
                    services.Remove(writeDescriptor);
                }

                var readDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ReadSharedHomeDbContext>));

                if (readDescriptor is not null)
                {
                    services.Remove(readDescriptor);
                }
                var settings = new SettingsProvider();

                services.AddSingleton(new SettingsProvider());                

                var mySQLSettings = settings.Get<MySQLOptions>(MySQLOptionsSetup.SectionName);

                services.AddDbContext<WriteSharedHomeDbContext>((serviceProvider, options) =>
                {
                    var auditableInterceptor = serviceProvider.GetService<AuditableInterceptor>()!;

                    options.UseMySql(mySQLSettings.ConnectionString, ServerVersion.AutoDetect(mySQLSettings.ConnectionString))
                        .AddInterceptors(auditableInterceptor);
                });

                services.AddDbContext<ReadSharedHomeDbContext>(options =>
                {
                    options.UseMySql(mySQLSettings.ConnectionString, ServerVersion.AutoDetect(mySQLSettings.ConnectionString));
                });

                services.AddTransient<BillSeed>();
                services.AddTransient<ShoppingListSeed>();
            });

        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
            _dbConnection = new MySqlConnection(_container.ConnectionString);

            HttpClient = CreateClient();

            await SeedDatabaseAsync();

            await InitializeRespawnerAsync();
        }

        public new async Task DisposeAsync() => await _container.StopAsync();


        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }
    
        private async Task SeedDatabaseAsync()
        {
            var writeContext = Services.GetRequiredService<WriteSharedHomeDbContext>();
            await TestDbInitializer.Initialize(writeContext);
        }

        private async Task InitializeRespawnerAsync()
        {
            await _dbConnection.OpenAsync();
            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.MySql,
                TablesToIgnore = new[] { new Table("Persons") },
            });
        }
    }
}
