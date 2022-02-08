using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Events;
using SharedHome.Domain.Bills.Exceptions;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Bills.Entities
{
    public class Bill : Entity, IAggregateRoot
    {
        public int Id { get; set; }

        public Guid PersonId { get; private set; }

        public bool IsPaid { get; private set; } = false;

        public BillType BillType { get; private set; }

        public ServiceProviderName ServiceProvider { get; private set; } = default!;

        public Money? Cost { get; private set; }

        public DateOnly DateOfPayment { get; private set; }

        private Bill()
        {

        }

        private Bill(Guid personId, BillType billType, ServiceProviderName serviceProvider, DateOnly dateOfPayment, Money? cost = null, bool isPaid = false)
        {
            PersonId = personId;
            BillType = billType;
            ServiceProvider = serviceProvider;
            DateOfPayment = dateOfPayment;
            Cost = cost;
            IsPaid = isPaid;
        }

        public static Bill Create(Guid personId, BillType billType, ServiceProviderName serviceProvider, DateOnly dateOfPayment, Money? cost = null, bool isPaid = false) =>
            new(personId, billType, serviceProvider, dateOfPayment, cost, isPaid);

        public void PayFor(Money cost)
        {
            IsAlreadyPaid();

            Cost = cost;
            IsPaid = true;
            AddEvent(new BillPaid(Id, ServiceProvider, Cost, DateOfPayment));
        }

        public void CancelPayment()
        {
            IsNotAlreadyPaid();

            Cost = null;
            IsPaid = false;
            AddEvent(new BillPaymentCanceled(Id, ServiceProvider, DateOfPayment));
        }

        public void ChangeCost(Money cost)
        {
            Cost = cost;
            AddEvent(new BillCostChanged(Id, ServiceProvider, Cost));
        }

        public void ChangeDateOfPayment(DateOnly dateOfPayment)
        {
            DateOfPayment = dateOfPayment;
            AddEvent(new BillDateOfPaymentChanged(Id, ServiceProvider, DateOfPayment));
        }

        public void IsAlreadyPaid()
        {
            if (IsPaid)
            {
                throw new BillPaidException();
            }
        }

        public void IsNotAlreadyPaid()
        {
            if (!IsPaid)
            {
                throw new BillNotPaidException();
            }
        }
    }
}
