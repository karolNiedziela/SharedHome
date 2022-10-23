using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetShoppingList : AuthorizeRequest, IQuery<Response<ShoppingListDto>>
    {
        public Guid Id { get; set; }
    }
}
