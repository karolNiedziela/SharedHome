using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Events;
using SharedHome.Domain.Bills.Exceptions;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace SharedHome.Domain.UnitTests.Bills
{
    public class BillTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Create_Throws_EmptyServiceProviderNameException_When_Name_Is_NullOrWhiteSpace(string name) 
        {
            var exception = Record.Exception(() 
                => Bill.Create(BillProvider.PersonId, BillType.Trash, name, DateTime.Now, new Money(100m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyServiceProviderNameException>();
        }

        [Fact]
        public void PayFor_Throws_BillPaidException_When_Bill_Is_Already_Paid()
        {
            var bill = BillProvider.Get(isPaid: true);

            var exception = Record.Exception(() => bill.PayFor(new Money(2500m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<BillPaidException>();
        }

        [Fact]
        public void PayFor_Adds_BillPaid_Event_On_Success()
        {
            var bill = BillProvider.Get();

            bill.PayFor(new Money(1500m, "zł"));

            bill.IsPaid.ShouldBeTrue();
            bill.Cost!.Amount.ShouldBe(1500m);
            bill.Events.Count().ShouldBe(1);

            var @event = bill.Events.FirstOrDefault() as BillPaid;
            @event.ShouldNotBeNull();

            @event.Cost.Amount.ShouldBe(1500m);
        }

        [Fact]
        public void CancelPayment_Throws_BillNotPaidException_When_Bill_Is_Not_Paid()
        {
            var bill = BillProvider.Get();

            var exception = Record.Exception(() => bill.CancelPayment());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<BillNotPaidException>();
        }

        [Fact]
        public void CancelPayment_Adds_BillPaymentCanceled_Event_On_Success()
        {
            var bill = BillProvider.Get(isPaid: true);

            bill.CancelPayment();

            bill.IsPaid.ShouldBeFalse();
            bill.Cost.ShouldBeNull();
            bill.Events.Count().ShouldBe(1);

            var @event = bill.Events.FirstOrDefault() as BillPaymentCanceled;
            @event.ShouldNotBeNull();
        }        

        [Fact]
        public void ChangeCost_Adds_BillCostChanged_Event_On_Success()
        {
            var bill = BillProvider.Get(isPaid: true);

            bill.ChangeCost(new Money(500m, "zł"));

            bill.Cost!.Amount.ShouldBe(500m);
            bill.Events.Count().ShouldBe(1);

            var @event = bill.Events.FirstOrDefault() as BillCostChanged;
            @event.ShouldNotBeNull();

            @event.Cost.Amount.ShouldBe(500m);
        }

        [Fact]
        public void ChangeDateOfPayments_Adds_BillDateOfPaymentChanged_Event_On_Success()
        {
            var bill = BillProvider.Get(isPaid: true);

            bill.ChangeDateOfPayment(new DateTime(2022, 5, 10));

            bill.DateOfPayment.ShouldBe(new DateTime(2022, 5, 10));
            bill.Events.Count().ShouldBe(1);

            var @event = bill.Events.FirstOrDefault() as BillDateOfPaymentChanged;
            @event.ShouldNotBeNull();

            @event.DateOfPayment.ShouldBe(new DateTime(2022, 5, 10));
        }
       
    }
}
