using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Application.Invitations.Exceptions
{
    public class InvitationAlreadySentException : SharedHomeException
    {
        public override string ErrorCode => "InvitationAlreadySent";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        [Order]
        public int HouseGroupId { get; }

        [Order(1)]
        public string RequestedToPersonId { get; }

        public InvitationAlreadySentException(int houseGroupId, string requestedToPersonId) 
            : base($"Invitation from house group with id '{houseGroupId}' already sent to person with id '{requestedToPersonId}'.")
        {
            HouseGroupId = houseGroupId;
            RequestedToPersonId = requestedToPersonId;
        }
    }
}
