using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class UpdateShoppingList : IAuthorizeRequest, ICommand<Unit>
    {
        public string? PersonId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
