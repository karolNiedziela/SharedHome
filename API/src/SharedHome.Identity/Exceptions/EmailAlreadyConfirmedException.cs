using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Identity.Exceptions
{
    public class EmailAlreadyConfirmedException : SharedHomeException
    {
        public override string ErrorCode => "EmailAlreadyConfirmed";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmailAlreadyConfirmedException() : base("Email address is already confirmed.")
        {
        }
    }
}
