using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class EmptyServiceProviderNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyServiceProviderName";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyServiceProviderNameException() : base($"Service provider name cannot be empty.")
        {
        }
    }
}
