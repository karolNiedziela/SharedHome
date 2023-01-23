using SharedHome.Api.Constants;
using SharedHome.Application.ShoppingLists.Commands.CancelPurchaseOfProduct;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.IntegrationTests.Extensions;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class CancelPurchaseOfProductControllerTests : ShoppingListControllerTestsBase
    {
        public CancelPurchaseOfProductControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Patch_CancePurchaseOfProduct_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListFakeProvider.GetWithProduct(
                price: new Money(100m,"zł"), 
                isBought: true, 
                shoppingListId: Guid.NewGuid(), 
                personId: TestDbInitializer.PersonId);
            await _shoppingListSeed.AddAsync(shoppingList);

            Authorize(userId: TestDbInitializer.PersonId);

            var command = new CancelPurchaseOfProductCommand
            {
                ProductName = "Product",
                ShoppingListId = shoppingList.Id
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.CancelPurchaseOfProduct.Replace("{shoppingListId}", shoppingList.Id.Value.ToString()).Replace("{productName}", "Product")}";

            var response = await Client.PatchAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
