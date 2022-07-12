using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.Shared.Exceptions
{
    public class InvalidCurrencyException : SharedHomeException
    {
        public override string ErrorCode => "InvalidCurrency";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidCurrencyException() : base("Invalid currency.")
        {

        }
    }
}
