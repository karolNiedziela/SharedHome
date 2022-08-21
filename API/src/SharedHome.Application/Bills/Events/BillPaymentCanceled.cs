using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.Bills.Events
{
    public record BillPaymentCanceled(int BillId, ServiceProviderName ServiceProviderName, DateTime DateOfPayment) : IDomainEvent;
}
