using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Infrastructure.Identity.Exceptions
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
