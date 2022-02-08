using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Events;
using SharedHome.Domain.Bills.Exceptions;
using SharedHome.Domain.Shared.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Domain.UnitTests.Bills
{
    public class BillTests
    {
        private Guid _personId = new("46826ecb-c40d-441c-ad0d-f11e616e4948");

        [Fact]
        public void PayFor_Throws_BillPaidException_When_Bill_Is_Already_Paid()
        {
            var bill = GetBill(true);

            var exception = Record.Exception(() => bill.PayFor(2500m));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<BillPaidException>();
        }

        [Fact]
        public void PayFor_Adds_BillPaid_Event_On_Success()
        {
            var bill = GetBill();

            bill.PayFor(1500m);

            bill.IsPaid.ShouldBeTrue();
            bill.Cost.ShouldBe(1500m);
            bill.Events.Count().ShouldBe(1);

            var @event = bill.Events.FirstOrDefault() as BillPaid;
            @event.ShouldNotBeNull();

            @event.Cost.ShouldBe(1500m);
        }

        [Fact]
        public void CancelPayment_Throws_BillNotPaidException_When_Bill_Is_Not_Paid()
        {
            var bill = GetBill();

            var exception = Record.Exception(() => bill.CancelPayment());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<BillNotPaidException>();
        }

        [Fact]
        public void CancelPayment_Adds_BillPaymentCanceled_Event_On_Success()
        {
            var bill = GetBill(true);

            bill.CancelPayment();

            bill.IsPaid.ShouldBeFalse();
            bill.Cost.ShouldBeNull();
            bill.Events.Count().ShouldBe(1);

            var @event = bill.Events.FirstOrDefault() as BillPaymentCanceled;
            @event.ShouldNotBeNull();
        }

        [Fact]
        public void ChangeCost_Throws_MoneyBelowZeroException_When_Value_Is_Lower_Than_Zero()
        {
            var bill = GetBill(true);

            var exception = Record.Exception(() => bill.ChangeCost(-200m));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<MoneyBelowZeroException>();
        }

        [Fact]
        public void ChangeCost_Adds_BillCostChanged_Event_On_Success()
        {
            var bill = GetBill(true);

            bill.ChangeCost(500m);

            bill.Cost.ShouldBe(500m);
            bill.Events.Count().ShouldBe(1);

            var @event = bill.Events.FirstOrDefault() as BillCostChanged;
            @event.ShouldNotBeNull();

            @event.Cost.ShouldBe(500m);
        }

        [Fact]
        public void ChangeDateOfPayments_Adds_BillDateOfPaymentChanged_Event_On_Success()
        {
            var bill = GetBill(true);

            bill.ChangeDateOfPayment(new DateOnly(2022, 5, 10));

            bill.DateOfPayment.ShouldBe(new DateOnly(2022, 5, 10));
            bill.Events.Count().ShouldBe(1);

            var @event = bill.Events.FirstOrDefault() as BillDateOfPaymentChanged;
            @event.ShouldNotBeNull();

            @event.DateOfPayment.ShouldBe(new DateOnly(2022, 5, 10));
        }

        private Bill GetBill(bool isPaid = false)
        {
            return Bill.Create(_personId, BillType.OTHER, "test", new DateOnly(2022, 10, 10), cost: 1000m, isPaid: isPaid);
        }
    }
}
