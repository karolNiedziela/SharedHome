﻿using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Tests.Shared.Providers
{
    public static class BillFakeProvider
    {
        public static readonly Guid BillId = new("082275f4-7566-41ab-8558-21c699939407");
        public static readonly Guid PersonId = new("c2506a12-41d4-4205-aafa-b835ae4bc057");
        public const string ServiceProviderName = "ProviderName";
        public static readonly DateOnly DateOfPayment = DateOnly.FromDateTime(new DateTime(2022, 3, 10));
        public static readonly Money DefaultBillCost = new(100m, "zł");

        public static Bill Get(BillType billType = BillType.Rent, Money? billCost = null,  bool isPaid = false, PersonId? personId = null, Guid? billId = null)
        {
            if (billCost is null)
            {
                return Bill.Create(billId ?? BillId, personId ?? PersonId, billType, ServiceProviderName,
                    DateOfPayment, DefaultBillCost, isPaid);
            }          

            return Bill.Create(billId ?? BillId, personId ?? PersonId, billType, ServiceProviderName,
                    DateOfPayment, billCost, isPaid);
        }
    }
}
