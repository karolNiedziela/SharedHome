using SharedHome.Api.Constants;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ShoppingLists.Commands.PurchaseProduct;
using SharedHome.IntegrationTests.Extensions;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class PurchaseShoppingListProductControllerTets : ShoppingListControllerTestsBase
    {
        public PurchaseShoppingListProductControllerTets(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Patch_PurchaseShoppingListProduct_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListFakeProvider.GetWithProduct(shoppingListId: Guid.NewGuid(), personId: TestDbInitializer.PersonId);
            await _shoppingListSeed.AddAsync(shoppingList);

            Authorize(userId: TestDbInitializer.PersonId);

            var command = new PurchaseProductCommand
            {
                Price = new MoneyDto(200, "zł"),
                ProductName = "Product",
                ShoppingListId = shoppingList.Id,
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.PurchaseShoppingListProduct.Replace("{shoppingListId}", shoppingList.Id.Value.ToString()).Replace("{productName}", "Product")}";

            var response = await Client.PatchAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
