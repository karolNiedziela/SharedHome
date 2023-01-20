using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Primivites;

namespace SharedHome.Domain.Bills.Events
{
    public record BillCreated(Guid BillId, ServiceProviderName ServiceProviderName, DateOnly DateOfPayment, Guid PersonId) : IDomainEvent;
}
