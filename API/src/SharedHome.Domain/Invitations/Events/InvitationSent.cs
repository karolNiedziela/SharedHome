using SharedHome.Domain.Primivites;

namespace SharedHome.Domain.Invitations.Events
{
    public record InvitationSent(Guid InvitationId, Guid HouseGroupId, Guid RequestedByPersonId, Guid RequestedToPersonId) : IDomainEvent;
}
