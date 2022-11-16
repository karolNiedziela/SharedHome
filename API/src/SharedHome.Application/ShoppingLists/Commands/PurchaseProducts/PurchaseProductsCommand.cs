using MediatR;
using SharedHome.Application.Common.DTO;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.PurchaseProducts
{
    public class PurchaseProductsCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public Dictionary<string, MoneyDto> PriceByProductNames { get; set; } = new();
    }
}
