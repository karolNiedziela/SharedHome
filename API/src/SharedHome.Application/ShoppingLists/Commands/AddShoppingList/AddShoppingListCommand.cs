using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingList
{
    public class AddShoppingListCommand : AuthorizeRequest, ICommand<Response<ShoppingListDto>>
    {
        public string Name { get; set; } = default!;

        public IReadOnlyList<AddShoppingListProductDto> Products { get; set; } = new List<AddShoppingListProductDto>();
    }
}
