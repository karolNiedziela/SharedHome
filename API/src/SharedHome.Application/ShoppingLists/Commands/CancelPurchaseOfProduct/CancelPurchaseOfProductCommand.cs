using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.CancelPurchaseOfProduct
{
    public class CancelPurchaseOfProductCommand : AuthorizeRequest, IRequest
    {
        public Guid ShoppingListId { get; set; }

        public string ProductName { get; set; } = default!;
    }
}
