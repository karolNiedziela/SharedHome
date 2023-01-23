using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.Exceptions;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
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
                => Bill.Create(Guid.NewGuid(), BillFakeProvider.PersonId, BillType.Trash, name, DateOnly.FromDateTime(DateTime.Now), new Money(100m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyServiceProviderNameException>();
        }

        [Fact]
        public void PayFor_Throws_BillPaidException_When_Bill_Is_Already_Paid()
        {
            var bill = BillFakeProvider.Get(isPaid: true);

            var exception = Record.Exception(() => bill.PayFor(new Money(2500m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<BillPaidException>();
        }

        [Fact]
        public void PayFor_Should_Set_Cost_And_IsPaid_To_True()
        {
            var bill = BillFakeProvider.Get();

            bill.PayFor(new Money(1500m, "zł"));

            bill.IsPaid.ShouldBeTrue();
            bill.Cost!.Amount.ShouldBe(1500m);
            bill.Cost!.Currency.Value.ShouldBe("zł");
        }

        [Fact]
        public void CancelPayment_Throws_BillNotPaidException_When_Bill_Is_Not_Paid()
        {
            var bill = BillFakeProvider.Get();

            var exception = Record.Exception(() => bill.CancelPayment());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<BillNotPaidException>();
        }

        [Fact]
        public void CancelPayment_Should_Clear_Cost_And_Set_IsPaid_To_False()
        {
            var bill = BillFakeProvider.Get(isPaid: true);

            bill.CancelPayment();

            bill.IsPaid.ShouldBeFalse();
            bill.Cost.ShouldBeNull();            
        }        

        [Fact]
        public void ChangeCost_Should_Change_Cost()
        {
            var bill = BillFakeProvider.Get(isPaid: true);

            bill.ChangeCost(new Money(500m, "zł"));

            bill.Cost!.Amount.ShouldBe(500m);
            bill.Cost!.Currency.Value.ShouldBe("zł");
        }

        [Fact]
        public void ChangeDateOfPayments_Should_Change_DateOfPayment()
        {
            var bill = BillFakeProvider.Get(isPaid: true);

            bill.ChangeDateOfPayment(DateOnly.FromDateTime(new DateTime(2022, 5, 10)));

            bill.DateOfPayment.ShouldBe(new DateOnly(2022, 5, 10));
        }
    }
}
