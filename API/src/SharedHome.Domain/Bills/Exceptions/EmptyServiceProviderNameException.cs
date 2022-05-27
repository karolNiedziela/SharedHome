using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class EmptyServiceProviderNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyServiceProviderName";

        public EmptyServiceProviderNameException() : base($"Service provider name cannot be empty.")
        {
        }
    }
}
