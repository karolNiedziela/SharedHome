using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.HouseGroups.Events
{
    public record HouseGroupOwnerChanged(int HouseGroupId, HouseGroupMember OldOwner, HouseGroupMember NewOwner) : IDomainEvent;
}
