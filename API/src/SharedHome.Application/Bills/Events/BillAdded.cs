using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.Bills.Events
{
    public record BillAdded(int Id, ServiceProviderName ServiceProviderName) : IDomainEvent;
}
