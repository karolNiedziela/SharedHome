using SharedHome.Domain.Common.Events;

namespace SharedHome.Application.Invitations.Events
{
    public record InvitationSent(Guid InvitationId, Guid HouseGroupId, Guid RequestByPersonId, Guid RequestToPersonId, string SentByFullName) : IDomainEvent;
}
