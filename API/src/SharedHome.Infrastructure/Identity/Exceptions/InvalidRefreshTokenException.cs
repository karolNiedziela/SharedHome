using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Infrastructure.Identity.Exceptions
{
    public class InvalidRefreshTokenException : SharedHomeException
    {
        public override string ErrorCode => "InvalidRefreshToken";

        public InvalidRefreshTokenException() : base("Refresh token was invalid.")
        {
        }
    }
}
