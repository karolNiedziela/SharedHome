using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.Shared.Exceptions
{
    public class MoneyAmountBelowZeroException : SharedHomeException
    {
        public override string ErrorCode => "MoneyAmountBelowZero";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public MoneyAmountBelowZeroException() : base("Price cannot be lower than zero.")
        {
        }
    }
}
