using SharedHome.Api.Constants;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class DeleteShoppingListProductControllerTests : ShoppingListControllerTestsBase
    {
        public DeleteShoppingListProductControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Delete_ShoppingListProduct_Should_Return_204_Status_Code()
        {
            var shoppingList = ShoppingListFakeProvider.GetWithProduct(personId: TestDbInitializer.PersonId, shoppingListId: Guid.NewGuid());
            await _shoppingListSeed.AddAsync(shoppingList);

            Authorize(userId: TestDbInitializer.PersonId);

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.Delete.Replace("{shoppingListId}", shoppingList.Id.Value.ToString()).Replace("{productName}", ShoppingListFakeProvider.ProductName)}";

            var response = await Client.DeleteAsync(endpointAddress);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
