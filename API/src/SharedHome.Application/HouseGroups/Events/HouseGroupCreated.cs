using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.HouseGroups.Events
{
    public record HouseGroupCreated(int HouseGroupId) : IDomainEvent;
}
