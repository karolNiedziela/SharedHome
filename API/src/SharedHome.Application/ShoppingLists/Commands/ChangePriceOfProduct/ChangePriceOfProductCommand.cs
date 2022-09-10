using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.ChangePriceOfProduct
{
    public class ChangePriceOfProductCommand : AuthorizeRequest, ICommand<Unit> 
    {
        public int ShoppingListId { get; set; }

        public string ProductName { get; set; } = default!;

        public MoneyDto? Price { get; set; }
    }
}
