using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Identity.Exceptions
{
    public class EmailNotConfirmedException : SharedHomeException
    {
        public override string ErrorCode => "EmailNotConfirmed";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmailNotConfirmedException() : base("Email address was not confirmed. Check your mailbox and confirm email.")
        {
        }
    }
}
