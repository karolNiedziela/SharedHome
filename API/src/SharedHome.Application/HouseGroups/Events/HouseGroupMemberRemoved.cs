using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.HouseGroups.Events
{
    public record HouseGroupMemberRemoved(int Id, string RemovedMemberId) : IDomainEvent;
}
