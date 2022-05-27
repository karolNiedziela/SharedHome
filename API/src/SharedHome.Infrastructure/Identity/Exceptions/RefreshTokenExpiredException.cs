using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Infrastructure.Identity.Exceptions
{
    public class RefreshTokenExpiredException : SharedHomeException
    {
        public override string ErrorCode => "RefreshTokenExpired";

        public RefreshTokenExpiredException() : base("Refresh token has expired.")
        {
        }
    }
}
