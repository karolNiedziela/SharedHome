using MediatR;
using SharedHome.Application.Common.DTO;
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

        public NetContentDto? NetContent { get; set; }

        public bool IsBought { get; set; }
    }
}
