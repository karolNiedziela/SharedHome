using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class HouseGroupNameEmptyException : SharedHomeException
    {
        public override string ErrorCode => "HouseGroupNameEmpty";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public HouseGroupNameEmptyException() : base("Name cannot be empty.")
        {

        }
    }
}
