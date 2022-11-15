using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.Common.Queries;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetAllShoppingListsByYearAndMonthAndIsDone :  AuthorizedPagedQuery<ShoppingListDto>
    {
        public int? Year { get; set; }

        public int? Month { get; set; }

        [BindRequired]
        public bool IsDone { get; set; }
    }
}
