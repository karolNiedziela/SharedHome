using SharedHome.Api.Constants;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ShoppingLists.Commands.ChangePriceOfProduct;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.IntegrationTests.Extensions;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class ChangePriceOfProductControllerTests : ShoppingListControllerTestsBase
    {
        public ChangePriceOfProductControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        public async Task Patch_ChangePriceOfProduct_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListFakeProvider.GetWithProduct(
                price: new Money(100m, "zł"), 
                isBought: true, 
                personId: TestDbInitializer.PersonId,
                shoppingListId: Guid.NewGuid());
            await _shoppingListSeed.AddAsync(shoppingList);

            Authorize(userId: TestDbInitializer.PersonId);

            var command = new ChangePriceOfProductCommand
            {
                Price = new MoneyDto(200, "zł"),
                ProductName = "Product",
                ShoppingListId = shoppingList.Id,
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.ChangePriceOfProduct.Replace("{shoppingListId}", shoppingList.Id.Value.ToString()).Replace("{productName}", "Product")}";

            var response = await Client.PatchAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
