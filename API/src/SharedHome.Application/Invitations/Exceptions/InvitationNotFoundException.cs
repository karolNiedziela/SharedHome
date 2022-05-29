using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.Invitations.Exceptions
{
    public class InvitationNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "InvitationNotFound";

        [Order]
        public int HouseGroupId { get; }

        public InvitationNotFoundException(int houseGroupId) 
            : base($"Invitation from house group with '{houseGroupId}' was not found.")
        {
            HouseGroupId = houseGroupId;
        }
    }
}
