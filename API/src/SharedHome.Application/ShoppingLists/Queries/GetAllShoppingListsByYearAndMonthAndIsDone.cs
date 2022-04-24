using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedHome.Application.DTO;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetAllShoppingListsByYearAndMonthAndIsDone : PagedQuery<ShoppingListDto>
    {
        public int? Year { get; set; }

        public int? Month { get; set; }

        [BindRequired]
        public bool IsDone { get; set; }
    }
}
