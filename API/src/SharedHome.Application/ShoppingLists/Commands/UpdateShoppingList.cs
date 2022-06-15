using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class UpdateShoppingList : AuthorizeRequest, ICommand<Unit>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
