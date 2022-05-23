using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Infrastructure.Identity.Exceptions
{
    public class RefreshTokenExpiredException : SharedHomeException
    {
        public RefreshTokenExpiredException() : base("Refresh token has expired.")
        {
        }
    }
}
