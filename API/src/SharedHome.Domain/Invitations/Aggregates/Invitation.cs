using SharedHome.Domain.Invitations.Exceptions;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Invitations.Aggregates
{
    public class Invitation : Entity, IAggregateRoot
    {
        public InvitationStatus Status { get; private set; }

        public int HouseGroupId { get; private set; } = default!;

        public Guid PersonId { get; private set; } = default!;


        private Invitation()
        {

        }

        private Invitation(int houseGroupId, Guid personId)
        {
            HouseGroupId = houseGroupId;
            HouseGroupId = houseGroupId;
            PersonId = personId;
            Status = InvitationStatus.PENDING;
        }

        public static Invitation Create(int houseGroupId, Guid personId)
            => new(houseGroupId, personId);

        public void Accept()
        {
            ThrowIfInvalidInvitationStatus(Status);

            Status = InvitationStatus.ACCEPTED;
        }

        public void Reject()
        {
            ThrowIfInvalidInvitationStatus(Status);

            Status = InvitationStatus.REJECTED;
        }

        // For changing status of invitation, it is invalid when invitation has different status than PENDING
        public static void ThrowIfInvalidInvitationStatus(InvitationStatus status)
        {
            switch (status)
            {
                case InvitationStatus.ACCEPTED:
                    throw new InvitationAcceptedException();

                case InvitationStatus.REJECTED:
                    throw new InvitationRejectedException();
            }
        }

    }
}
