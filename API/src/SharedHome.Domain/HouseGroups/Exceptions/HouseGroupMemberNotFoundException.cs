using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class HouseGroupMemberNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "HouseGroupMemberNotFound";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        [Order]
        public Guid PersonId { get; }

        public HouseGroupMemberNotFoundException(Guid personId) : base($"House group member with person id: '{personId}' was not found.")
        {
            PersonId = personId;
        }

    }
}
