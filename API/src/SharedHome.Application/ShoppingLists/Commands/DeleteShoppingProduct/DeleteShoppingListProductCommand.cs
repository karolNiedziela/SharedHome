using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.DeleteShoppingListProduct
{
    public class DeleteShoppingListProductCommand : AuthorizeRequest, ICommand<Unit>
    {
         public Guid ShoppingListId { get; set; }

         public string ProductName { get; set; } = default!;
    }
}
