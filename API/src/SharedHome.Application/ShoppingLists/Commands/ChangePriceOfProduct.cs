using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class ChangePriceOfProduct : AuthorizeRequest, ICommand<Unit> 
    {
        public int ShoppingListId { get; set; }

        public string ProductName { get; set; } = default!;

        public decimal Price { get; set; }

        public string Currency { get; set; } = default!;
    }
}
