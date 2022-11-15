﻿using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.ChangePriceOfProduct
{
    public class ChangePriceOfProductCommand : AuthorizeRequest, ICommand<Unit> 
    {
        public Guid ShoppingListId { get; set; }

        public string ProductName { get; set; } = default!;

        public MoneyDto? Price { get; set; }
    }
}
