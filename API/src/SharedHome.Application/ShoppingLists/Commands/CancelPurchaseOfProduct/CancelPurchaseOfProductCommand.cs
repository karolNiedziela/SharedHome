using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.CancelPurchaseOfProduct
{
    public class CancelPurchaseOfProductCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public string ProductName { get; set; } = default!;
    }
}
