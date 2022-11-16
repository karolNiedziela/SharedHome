using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillPaidException : SharedHomeException
    {
        public override string ErrorCode => "BillPaid";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public BillPaidException() : base($"Bill is already paid.")
        {
        }
    }
}
