using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class AddShoppingList : AuthorizeCommand, ICommand<Unit>
    {
        public string Name { get; set; } = default!;
    }
}
