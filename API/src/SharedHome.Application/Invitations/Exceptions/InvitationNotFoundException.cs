using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.Invitations.Exceptions
{
    public class InvitationNotFoundException : SharedHomeException
    {
        public int HouseGroupId { get; }

        public InvitationNotFoundException(int houseGroupId) 
            : base($"Invitation from house group with '{houseGroupId}' was not found.")
        {
            HouseGroupId = houseGroupId;
        }
    }
}
