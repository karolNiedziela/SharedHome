﻿using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class CancelPurchaseOfProduct : AuthorizeRequest, ICommand<Unit>
    {
        public int ShoppingListId { get; set; }

        public string ProductName { get; set; } = default!;
    }
}
