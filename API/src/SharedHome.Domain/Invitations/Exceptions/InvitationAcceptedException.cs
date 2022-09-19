using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Invitations.Exceptions
{
    public class InvitationAcceptedException : SharedHomeException
    {
        public override string ErrorCode => "InvitationAccepted";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public InvitationAcceptedException() : base($"Invitation is already accepted.")
        {
        }
    }
}
