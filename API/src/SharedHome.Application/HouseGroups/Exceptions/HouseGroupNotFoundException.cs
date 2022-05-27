using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class HouseGroupNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "HouseGroupNotFoundException";

        public int HouseGroupId { get; }

        public HouseGroupNotFoundException(int houseGroupId) : base($"House group with id '{houseGroupId}' was not found.")
        {
            HouseGroupId = houseGroupId;
        }
    }
}
