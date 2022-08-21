using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.Bills.Events
{
    public record BillPaymentCanceled(int BillId, ServiceProviderName ServiceProviderName, DateTime DateOfPayment) : IDomainEvent;
}
