using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillNotPaidException : SharedHomeException
    {
        public override string ErrorCode => "BillNotPaid";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public BillNotPaidException() : base($"Bill has not been paid yet.")
        {
        }
    }
}
