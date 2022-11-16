using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class HouseGroupNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "HouseGroupNotFound";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        [Order]
        public Guid HouseGroupId { get; }

        public HouseGroupNotFoundException(Guid houseGroupId) : base($"House group with id '{houseGroupId}' was not found.")
        {
            HouseGroupId = houseGroupId;
        }
    }
}
