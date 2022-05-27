using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Invitations.Exceptions
{
    public class InvitationRejectedException : SharedHomeException
    {
        public override string ErrorCode => "InvitationRejected";

        public InvitationRejectedException() : base($"Friend request was rejected.")
        {
        }
    }
}
