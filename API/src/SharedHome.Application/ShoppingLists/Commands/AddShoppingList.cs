using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class AddShoppingList : AuthorizeRequest, ICommand<Response<ShoppingListDto>>
    {
        public string Name { get; set; } = default!;

        public IEnumerable<AddShoppingListShoppingListProduct> Products { get; set; } = new List<AddShoppingListShoppingListProduct>();
    }

    public class AddShoppingListShoppingListProduct
    {
        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public string? NetContent { get; set; }

        public int? NetContentType { get; set; } = default!;
    }
}
