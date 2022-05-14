using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Exceptions;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.Invitations.Aggregates
{
    public class Invitation : Entity, IAggregateRoot
    {
        public int Id { get; set; }

        public InvitationStatus Status { get; private set; }

        public int HouseGroupId { get; private set; } = default!;

        public string RequestedByPersonId { get; private set; } = default!;

        public string RequestedToPersonId { get; private set; } = default!;


        private Invitation()
        {

        }

        private Invitation(int houseGroupId, string requestedByPersonId, string requestedToPersonId)
        {
            HouseGroupId = houseGroupId;
            RequestedByPersonId = requestedByPersonId;
            RequestedToPersonId = requestedToPersonId;
            Status = InvitationStatus.Pending;
        }

        public static Invitation Create(int houseGroupId, string requestedByPersonId, string requestedToPersonId)
            => new(houseGroupId, requestedByPersonId, requestedToPersonId);

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
        public static void ThrowIfInvalidInvitationStatus(InvitationStatus status)
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
