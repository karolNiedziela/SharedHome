using SharedHome.Api.Constants;
using SharedHome.Application.ShoppingLists.Commands.AddShoppingListProducts;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class CreateShoppingListProductControllerTests : ShoppingListControllerTestsBase
    {
        public CreateShoppingListProductControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Put_AddShoppingListProduct_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(personId: TestDbInitializer.PersonId, shoppingListId: Guid.NewGuid());
            await _shoppingListSeed.AddAsync(shoppingList);

            Authorize(userId: TestDbInitializer.PersonId);

            var command = new AddShoppingListProductsCommand
            {
                ShoppingListId = shoppingList.Id,
                Products = new List<AddShoppingListProductDto>
                        {
                            new AddShoppingListProductDto
                            {
                                Name = "Product",
                                Quantity = 2,
                            }
                        }
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.AddShoppingListProduct.Replace("{shoppingListId}", shoppingList.Id.Value.ToString())}";
            var response = await Client.PutAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
