using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetShoppingList : IAuthorizeRequest, IQuery<Response<ShoppingListDto>>
    {
        public int Id { get; set; }

        public string? PersonId { get; set; }
    }
}
