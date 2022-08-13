﻿using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands.ChangeBillCost
{
    public class ChangeBillCostCommand : AuthorizeRequest, ICommand<Unit>
    {
        public int BillId { get; set; }

        public decimal Cost { get; set; }

        public string Currency { get; set; } = default!;
    }
}