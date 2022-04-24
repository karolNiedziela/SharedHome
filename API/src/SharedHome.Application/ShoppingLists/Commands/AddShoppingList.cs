using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class AddShoppingList : AuthorizeRequest, ICommand<Unit>
    {
        public string Name { get; set; } = default!;
    }
}
