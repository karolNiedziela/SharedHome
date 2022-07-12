using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class LeavingHouseGroupNewOwnerNotDefinedException : SharedHomeException
    {
        public override string ErrorCode => "LeavingHouseGroupNewOwnerNotDefined";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public LeavingHouseGroupNewOwnerNotDefinedException() : base("As owner you have to choose new owner.")
        {
        }
    }
}
