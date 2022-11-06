using Microsoft.Extensions.DependencyInjection;
using SharedHome.IntegrationTests.DataSeeds;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    [Collection("Shared test collection")]
    public abstract class ShoppingListControllerTestsBase : ControllerTests, IAsyncLifetime
    {
        protected const string BaseAddress = "api/shoppinglists";

        protected readonly Func<Task> _resetDatabase;
        protected readonly ShoppingListSeed _shoppingListSeed;

        public ShoppingListControllerTestsBase(CustomWebApplicationFactory factory) : base(factory)
        {
            _shoppingListSeed = factory.Services.GetRequiredService<ShoppingListSeed>();
            _resetDatabase = factory.ResetDatabaseAsync;
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public Task DisposeAsync() => _resetDatabase();
    }
}
