using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class LeavingHouseGroupNewOwnerNotDefinedException : SharedHomeException
    {
        public override string ErrorCode => "LeavingHouseGroupNewOwnerNotDefined";

        public LeavingHouseGroupNewOwnerNotDefinedException() : base("As owner you have to choose new owner.")
        {
        }
    }
}
