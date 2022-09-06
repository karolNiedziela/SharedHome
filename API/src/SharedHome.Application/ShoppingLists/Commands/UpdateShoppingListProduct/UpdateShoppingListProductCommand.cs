using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.UpdateShoppingListProduct
{
    public class UpdateShoppingListProductCommand : AuthorizeRequest, ICommand<Unit>
    {
        public int ShoppingListId { get; set; }

        public string CurrentProductName { get; set; } = default!;

        public string NewProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public string? NetContent { get; set; }

        public int? NetContentType { get; set; } = default!;

        public bool IsBought { get; set; }
    }
}
