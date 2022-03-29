using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class HouseGroupNotFoundException : SharedHomeException
    {
        private readonly int houseGroupId;

        public HouseGroupNotFoundException(int houseGroupId) : base($"House group with id '{houseGroupId}' was not found.")
        {
            this.houseGroupId = houseGroupId;
        }
    }
}
