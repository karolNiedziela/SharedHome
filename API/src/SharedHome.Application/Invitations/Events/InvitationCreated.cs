using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.HouseGroups.Events
{
    public record InvitationCreated(int HouseGroupId, Guid PersonId) : IDomainEvent;
}
