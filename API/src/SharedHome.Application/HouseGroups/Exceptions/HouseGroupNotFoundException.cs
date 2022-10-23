using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
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
