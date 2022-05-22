using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class AddShoppingList : IAuthorizeRequest, ICommand<Unit>
    {
        public string Name { get; set; } = default!;

        public string? PersonId { get; set; }
    }
}
