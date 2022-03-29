using SharedHome.Application.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetShoppingList : AuthorizeCommand, IQuery<Response<ShoppingListDto>>
    {
        public int Id { get; set; }
    }
}
