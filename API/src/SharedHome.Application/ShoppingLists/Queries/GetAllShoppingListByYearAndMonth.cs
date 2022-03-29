using SharedHome.Application.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetAllShoppingListByYearAndMonth : AuthorizeCommand, IQuery<Response<IEnumerable<ShoppingListDto>>>
    {
        public int Year { get; set; }

        public int Month { get; set; }
    }
}
