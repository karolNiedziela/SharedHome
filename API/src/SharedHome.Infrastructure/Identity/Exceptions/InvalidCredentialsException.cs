using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Infrastructure.Identity.Exceptions
{
    public class InvalidCredentialsException : SharedHomeException
    {
        public override string ErrorCode => "InvalidCredentials";

        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}
