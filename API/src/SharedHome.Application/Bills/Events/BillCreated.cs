using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Common.Events;

namespace SharedHome.Application.Bills.Events
{
    public record BillCreated(Guid BillId, ServiceProviderName ServiceProviderName, DateOnly DateOfPayment, CreatorDto Creator) : IDomainEvent;
}
