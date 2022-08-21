using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.HouseGroups.Events
{
    public record HouseGroupMemberAdded(int HouseGroupId, HouseGroupMember HouseGroupMember) : IDomainEvent;
}
