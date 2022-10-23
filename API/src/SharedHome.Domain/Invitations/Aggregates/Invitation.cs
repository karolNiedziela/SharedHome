using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Exceptions;
using SharedHome.Domain.Invitations.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.Invitations.Aggregates
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

        private Invitation(InvitationId id, HouseGroupId houseGroupId, PersonId requestedByPersonId, PersonId requestedToPersonId)
        {
            Id = id;
            HouseGroupId = houseGroupId;
            RequestedByPersonId = requestedByPersonId;
            RequestedToPersonId = requestedToPersonId;
            Status = InvitationStatus.Pending;
        }

        public static Invitation Create(InvitationId id, HouseGroupId houseGroupId, PersonId requestedByPersonId, PersonId requestedToPersonId)
            => new(id, houseGroupId, requestedByPersonId, requestedToPersonId);

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
