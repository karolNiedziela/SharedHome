using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.Events;
using SharedHome.Domain.Bills.Exceptions;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Primivites;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.Bills
{
    public class Bill : AggregateRoot<BillId>
    {
        public bool IsPaid { get; private set; } = false;

        public BillType BillType { get; private set; }

        public ServiceProviderName ServiceProvider { get; private set; } = default!;

        public Money? Cost { get; private set; }

        public DateOnly DateOfPayment { get; private set; }

        public PersonId PersonId { get; private set; } = default!;

        private Bill()
        {

        }

        private Bill(BillId id, PersonId personId, BillType billType, ServiceProviderName serviceProvider, DateOnly dateOfPayment, Money? cost = null, bool isPaid = false)
        {
            Id = id;
            PersonId = personId;
            BillType = billType;
            ServiceProvider = serviceProvider;
            DateOfPayment = dateOfPayment;
            Cost = cost;
            IsPaid = isPaid;

            AddEvent(new BillCreated(id, serviceProvider, dateOfPayment, personId));
        }

        public static Bill Create(BillId id, PersonId personId, BillType billType, ServiceProviderName serviceProvider, DateOnly dateOfPayment, Money? cost = null, bool isPaid = false) =>
            new(id, personId, billType, serviceProvider, dateOfPayment, cost, isPaid);

        public void PayFor(Money cost)
        {
            IsAlreadyPaid();

            Cost = cost;
            IsPaid = true;            
        }

        public void CancelPayment()
        {
            IsNotAlreadyPaid();

            Cost = null;
            IsPaid = false;
        }

        public void ChangeCost(Money cost)
        {
            Cost = cost;            
        }

        public void ChangeDateOfPayment(DateOnly dateOfPayment)
        {
            DateOfPayment = dateOfPayment;            
        }

        public void Update(BillType billType, ServiceProviderName serviceProvider, DateOnly dateOfPayment, Money? cost)
        {
            BillType = billType;
            ServiceProvider = serviceProvider;
            DateOfPayment = dateOfPayment;
            Cost = cost;
        }

        private void IsAlreadyPaid()
        {
            if (IsPaid)
            {
                throw new BillPaidException();
            }
        }

        private void IsNotAlreadyPaid()
        {
            if (!IsPaid)
            {
                throw new BillNotPaidException();
            }
        }
    }
}
