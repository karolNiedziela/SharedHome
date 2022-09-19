using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.Invitations.Exceptions
{
    public class InvitationRejectedException : SharedHomeException
    {
        public override string ErrorCode => "InvitationRejected";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public InvitationRejectedException() : base($"Invitation is already rejected.")
        {
        }
    }
}
