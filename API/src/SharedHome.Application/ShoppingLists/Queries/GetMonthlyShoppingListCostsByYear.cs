﻿using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetMonthlyShoppingListCostsByYear : IAuthorizeRequest, IQuery<Response<List<ShoppingListMonthlyCostDto>>>
    {
        public int? Year { get; set; }

        public string? PersonId { get; set; }
    }
}
