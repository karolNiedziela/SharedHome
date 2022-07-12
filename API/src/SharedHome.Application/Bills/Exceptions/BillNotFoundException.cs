using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Application.Bills.Exceptions
{
    public class BillNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "BillNotFound";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        [Order]
        public int BillId { get; }

        public BillNotFoundException(int billId) : base($"Bill with id '{billId}' was not found.")
        {
            BillId = billId;
        }
    }
}
