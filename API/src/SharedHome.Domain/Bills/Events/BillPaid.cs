using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.Bills.Events
{
    public record BillPaid(int BillId, ServiceProviderName ServiceProviderName, Money Cost, DateTime DateOfPayment) : IDomainEvent;
}
