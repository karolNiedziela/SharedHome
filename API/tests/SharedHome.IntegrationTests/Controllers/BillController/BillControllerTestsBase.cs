using Microsoft.Extensions.DependencyInjection;
using SharedHome.IntegrationTests.DataSeeds;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.BillController
{
    [Collection("Shared test collection")]
    public class BillControllerTestsBase : ControllerTests, IAsyncLifetime
    {
        protected const string BaseAddress = "api/bills";

        protected readonly Func<Task> _resetDatabase;
        protected readonly BillSeed _billSeed;

        public BillControllerTestsBase(CustomWebApplicationFactory factory) : base(factory)
        {
            _billSeed = factory.Services.GetRequiredService<BillSeed>();
            _resetDatabase = factory.ResetDatabaseAsync;
        }

        public Task InitializeAsync() => Task.CompletedTask;
        
        public Task DisposeAsync() => _resetDatabase();        
    }
}
