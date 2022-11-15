using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.Common.Requests;
using SharedHome.Application.Common.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetShoppingList : AuthorizeRequest, IQuery<Response<ShoppingListDto>>
    {
        public Guid Id { get; set; }
    }
}
