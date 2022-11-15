using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.UpdateShoppingListProduct
{
    public class UpdateShoppingListProductCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public string CurrentProductName { get; set; } = default!;

        public string NewProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public NetContentDto? NetContent { get; set; }

        public bool IsBought { get; set; }
    }
}
