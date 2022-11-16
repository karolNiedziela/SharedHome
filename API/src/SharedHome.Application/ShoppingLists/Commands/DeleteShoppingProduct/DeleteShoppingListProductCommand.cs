using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.DeleteShoppingListProduct
{
    public class DeleteShoppingListProductCommand : AuthorizeRequest, IRequest<Unit>
    {
         public Guid ShoppingListId { get; set; }

         public string ProductName { get; set; } = default!;
    }
}
