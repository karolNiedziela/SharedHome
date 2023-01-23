using SharedHome.Application.ShoppingLists.Commands.UpdateShoppingList;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class UpdateShoppingListControllerTests : ShoppingListControllerTestsBase
    {
        public UpdateShoppingListControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task Put_Update_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListFakeProvider.GetEmpty(personId: TestDbInitializer.PersonId, shoppingListId: Guid.NewGuid());
            await _shoppingListSeed.AddAsync(shoppingList);

            Authorize(userId: TestDbInitializer.PersonId);

            var command = new UpdateShoppingListCommand
            {
                ShoppingListId = shoppingList.Id.Value,
                Name = "NewShoppingList"
            };

            var endpointAddress = $"{BaseAddress}";

            var response = await Client.PutAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
