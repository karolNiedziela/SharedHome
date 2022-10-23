﻿using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands.PayForBill
{
    public class PayForBillCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid BillId { get; set; }

        public MoneyDto Cost { get; set; } = default!;
    }
}
