using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.ValueObjects;
using System;

namespace SharedHome.Application.UnitTests.Providers
{
    public static class BillProvider
    {
        public const string DefaultPersonId = "c2506a12-41d4-4205-aafa-b835ae4bc057";
        public const string DefaultServiceProviderName = "ProviderName";
        public static readonly DateOnly DefaultDateOfPayment = new(2022, 3, 10);

        public static Bill Get(BillType billType = BillType.Rent, BillCost? billCost = null, bool isPaid = false)
            => Bill.Create(DefaultPersonId, billType, DefaultServiceProviderName,
                DefaultDateOfPayment, billCost, isPaid);

    }
}
