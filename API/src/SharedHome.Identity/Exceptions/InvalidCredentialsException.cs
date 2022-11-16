using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Identity.Exceptions
{
    public class InvalidCredentialsException : SharedHomeException
    {
        public override string ErrorCode => "InvalidCredentials";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}
