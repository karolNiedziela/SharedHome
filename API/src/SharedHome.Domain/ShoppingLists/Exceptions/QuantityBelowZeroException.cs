using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class QuantityBelowZeroException : SharedHomeException
    {
        public override string ErrorCode => "QuantityBelowZero";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public QuantityBelowZeroException() : base("Quantity cannot be lower than zero.")
        {
        }
    }
}
