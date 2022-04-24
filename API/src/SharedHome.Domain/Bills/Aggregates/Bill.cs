using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Events;
using SharedHome.Domain.Bills.Exceptions;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.Bills.Entities
{
    public class Bill : Entity, IAggregateRoot
    {
        public int Id { get; set; }

        public string PersonId { get; private set; } = default!;

        public bool IsPaid { get; private set; } = false;

        public BillType BillType { get; private set; }

        public ServiceProviderName ServiceProvider { get; private set; } = default!;

        public BillCost? Cost { get; private set; }

        public DateTime DateOfPayment { get; private set; }

        private Bill()
        {

        }

        private Bill(string personId, BillType billType, ServiceProviderName serviceProvider, DateTime dateOfPayment, BillCost? cost = null, bool isPaid = false)
        {
            PersonId = personId;
            BillType = billType;
            ServiceProvider = serviceProvider;
            DateOfPayment = dateOfPayment;
            Cost = cost;
            IsPaid = isPaid;
        }

        public static Bill Create(string personId, BillType billType, ServiceProviderName serviceProvider, DateTime dateOfPayment, BillCost? cost = null, bool isPaid = false) =>
            new(personId, billType, serviceProvider, dateOfPayment, cost, isPaid);

        public void PayFor(BillCost cost)
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

        public void ChangeCost(BillCost cost)
        {
            Cost = cost;
            AddEvent(new BillCostChanged(Id, ServiceProvider, Cost));
        }

        public void ChangeDateOfPayment(DateTime dateOfPayment)
        {
            DateOfPayment = dateOfPayment;
            AddEvent(new BillDateOfPaymentChanged(Id, ServiceProvider, DateOfPayment));
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
