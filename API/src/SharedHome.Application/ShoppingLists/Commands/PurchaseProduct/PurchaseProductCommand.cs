using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.PurchaseProduct
{
    public class PurchaseProductCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public string ProductName { get; set; } = default!;

        public MoneyDto Price { get; set; } = default!;
    }
}
