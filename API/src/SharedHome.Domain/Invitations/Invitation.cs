using SharedHome.Domain.Invitations.Enums;
using SharedHome.Domain.Invitations.Events;
using SharedHome.Domain.Invitations.Exceptions;
using SharedHome.Domain.Invitations.ValueObjects;
using SharedHome.Domain.Primivites;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.Invitations
{
    public class Invitation : AggregateRoot<InvitationId>
    {
        public InvitationStatus Status { get; private set; }

        public HouseGroupId HouseGroupId { get; private set; } = default!;

        public PersonId RequestedByPersonId { get; private set; } = default!;

        public PersonId RequestedToPersonId { get; private set; } = default!;

        private Invitation()
        {

        }

        private Invitation(InvitationId id, HouseGroupId houseGroupId, PersonId requestedByPersonId, PersonId requestedToPersonId, InvitationStatus invitationStatus)
        {
            Id = id;
            HouseGroupId = houseGroupId;
            RequestedByPersonId = requestedByPersonId;
            RequestedToPersonId = requestedToPersonId;
            Status = invitationStatus;
        }
        public static Invitation CreateSent(InvitationId id, HouseGroupId houseGroupId, PersonId requestedByPersonId, PersonId requestedToPersonId)
            => new(id, houseGroupId, requestedByPersonId, requestedToPersonId, InvitationStatus.Sent);

        public static Invitation CreatePending(InvitationId id, HouseGroupId houseGroupId, PersonId requestedByPersonId, PersonId requestedToPersonId)
        {
            var invitation = new Invitation(id, houseGroupId, requestedByPersonId, requestedToPersonId, InvitationStatus.Pending);

            invitation.AddEvent(new InvitationSent(id, houseGroupId, requestedByPersonId, requestedToPersonId));

            return invitation;
        }

        public void Accept()
        {
            ThrowIfInvalidInvitationStatus(Status);

            Status = InvitationStatus.Accepted;
        }

        public void Reject()
        {
            ThrowIfInvalidInvitationStatus(Status);

            Status = InvitationStatus.Rejected;
        }

        // For changing status of invitation, it is invalid when invitation has different status than PENDING
        private static void ThrowIfInvalidInvitationStatus(InvitationStatus status)
        {
            switch (status)
            {
                case InvitationStatus.Accepted:
                    throw new InvitationAcceptedException();

                case InvitationStatus.Rejected:
                    throw new InvitationRejectedException();
            }
        }

    }
}
