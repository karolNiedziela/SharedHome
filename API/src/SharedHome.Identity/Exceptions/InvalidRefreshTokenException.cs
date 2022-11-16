using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Identity.Exceptions
{
    public class InvalidRefreshTokenException : SharedHomeException
    {
        public override string ErrorCode => "InvalidRefreshToken";

        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public InvalidRefreshTokenException() : base("Refresh token was invalid.")
        {
        }
    }
}
