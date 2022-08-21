using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.HouseGroups.Events
{
    public record HouseGroupMemberRemoved(int Id, string RemovedMemberId) : IDomainEvent;
}
