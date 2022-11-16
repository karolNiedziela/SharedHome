using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Application.Invitations.Exceptions
{
    public class InvitationAlreadySentException : SharedHomeException
    {
        public override string ErrorCode => "InvitationAlreadySent";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        [Order]
        public Guid HouseGroupId { get; }

        [Order(1)]
        public Guid RequestedToPersonId { get; }

        public InvitationAlreadySentException(Guid houseGroupId, Guid requestedToPersonId) 
            : base($"Invitation from house group with id '{houseGroupId}' already sent to person with id '{requestedToPersonId}'.")
        {
            HouseGroupId = houseGroupId;
            RequestedToPersonId = requestedToPersonId;
        }
    }
}
