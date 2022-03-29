using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class DeleteShoppingListProduct : AuthorizeCommand, ICommand<Unit>
    {
         public int ShoppingListId { get; set; }

         public string ProductName { get; set; } = default!;
    }
}
