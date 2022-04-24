using SharedHome.Domain.HouseGroups.ValueObjects;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Exceptions;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.Invitations.Aggregates
{
    public class Invitation : Entity, IAggregateRoot
    {
        public InvitationStatus Status { get; private set; }

        public int HouseGroupId { get; private set; } = default!;

        public FirstName SentByFirstName { get; private set; } = default!;

        public LastName SentByLastName { get; private set; } = default!;

        public string PersonId { get; private set; } = default!;


        private Invitation()
        {

        }

        private Invitation(int houseGroupId, string personId, FirstName sentByFirstName, LastName sentByLastName)
        {
            HouseGroupId = houseGroupId;
            PersonId = personId;
            SentByFirstName = sentByFirstName;
            SentByLastName = sentByLastName;
            Status = InvitationStatus.Pending;
        }

        public static Invitation Create(int houseGroupId, string personId, 
            FirstName sentByFirstName, LastName sentByLastName)
            => new(houseGroupId, personId, sentByFirstName, sentByLastName);

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
