using SharedHome.Api.Constants;
using SharedHome.Application.ShoppingLists.Commands.SetIsShoppingListDone;
using SharedHome.IntegrationTests.Extensions;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class SetIsDoneShoppingListControllerTests : ShoppingListControllerTestsBase
    {
        public SetIsDoneShoppingListControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Patch_SetIsDone_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(personId: TestDbInitializer.PersonId, shoppingListId: Guid.NewGuid());
            await _shoppingListSeed.AddAsync(shoppingList);

            Authorize(userId: TestDbInitializer.PersonId);

            var command = new SetIsShoppingListDoneCommand
            {
                IsDone = true,
                ShoppingListId = shoppingList.Id
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.SetIsDone.Replace("{shoppingListId:int}", shoppingList.Id.Value.ToString())}";

            var response = await Client.PatchAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
