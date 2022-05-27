using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.Invitations.Exceptions
{
    public class InvitationAlreadySentException : SharedHomeException
    {
        public override string ErrorCode => "InvitationAlreadySent";

        public int HouseGroupId { get; }

        public string RequestedToPersonId { get; }

        public InvitationAlreadySentException(int houseGroupId, string requestedToPersonId) 
            : base($"Invitation from house group with id '{houseGroupId}' already sent to person with id '{requestedToPersonId}'.")
        {
            HouseGroupId = houseGroupId;
            RequestedToPersonId = requestedToPersonId;
        }
    }
}
