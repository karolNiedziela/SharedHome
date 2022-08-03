using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Identity.Exceptions
{
    public class RefreshTokenExpiredException : SharedHomeException
    {
        public override string ErrorCode => "RefreshTokenExpired";

        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public RefreshTokenExpiredException() : base("Refresh token has expired.")
        {
        }
    }
}
