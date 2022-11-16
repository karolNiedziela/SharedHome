using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Application.Invitations.Exceptions
{
    public class InvitationNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "InvitationNotFound";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        [Order]
        public Guid HouseGroupId { get; }

        public InvitationNotFoundException(Guid houseGroupId) 
            : base($"Invitation from house group with id '{houseGroupId}' was not found.")
        {
            HouseGroupId = houseGroupId;
        }
    }
}
