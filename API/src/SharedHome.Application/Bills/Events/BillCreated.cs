using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.Bills.Events
{
    public record BillCreated(int Id, ServiceProviderName ServiceProviderName, CreatorDto Creator) : IDomainEvent;
}
