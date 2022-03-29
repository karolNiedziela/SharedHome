using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.Invitations.Exceptions
{
    public class InvitationAlreadySentException : SharedHomeException
    {
        public InvitationAlreadySentException(int houseGroupId, string personId) 
            : base($"Invitation from house group with id '{houseGroupId}' already sent to person with id '{personId}'.")
        {
            HouseGroupId = houseGroupId;
            PersonId = personId;
        }

        public int HouseGroupId { get; }
        public string PersonId { get; }
    }
}
