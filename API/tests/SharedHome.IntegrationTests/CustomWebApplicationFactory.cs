using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Infrastructure.EF.Contexts;
using System;
using System.Linq;
using System.Net.Http;

namespace SharedHome.IntegrationTests
{
    internal sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
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
            });
        }

        public CustomWebApplicationFactory(Action<IServiceCollection>? services = null)
        {
            Client = WithWebHostBuilder(builder =>
           {
               if (services is not null)
               {
                   builder.ConfigureServices(services!);
               }

               builder.UseEnvironment("Test");
           }).CreateClient();
        }
    }
}
