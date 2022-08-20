using MediatR;
using SharedHome.Application.Common.Models;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.PurchaseProducts
{
    public class PurchaseProductsCommand : AuthorizeRequest, ICommand<Unit>
    {
        public int ShoppingListId { get; set; }

        public Dictionary<string, MoneyModel> PriceByProductNames { get; set; } = new();
    }
}
