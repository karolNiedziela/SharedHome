﻿using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.ValueObjects;

namespace SharedHome.Tests.Shared.Providers
{
    public static class BillProvider
    {
        public const string PersonId = "c2506a12-41d4-4205-aafa-b835ae4bc057";
        public const string ServiceProviderName = "ProviderName";
        public static readonly DateTime DateOfPayment = new(2022, 3, 10);

        public static Bill Get(BillType billType = BillType.Rent, BillCost? billCost = null, bool isPaid = false)
            => Bill.Create(PersonId, billType, ServiceProviderName,
                DateOfPayment, billCost, isPaid);

    }
}
