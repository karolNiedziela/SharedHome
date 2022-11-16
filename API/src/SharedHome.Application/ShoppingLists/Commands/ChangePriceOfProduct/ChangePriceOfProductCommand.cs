using MediatR;
using SharedHome.Application.Common.DTO;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.ChangePriceOfProduct
{
    public class ChangePriceOfProductCommand : AuthorizeRequest, IRequest<Unit> 
    {
        public Guid ShoppingListId { get; set; }

        public string ProductName { get; set; } = default!;

        public MoneyDto? Price { get; set; }
    }
}
