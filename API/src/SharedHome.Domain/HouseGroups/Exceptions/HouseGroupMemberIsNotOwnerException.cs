using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class HouseGroupMemberIsNotOwnerException : SharedHomeException
    {
        public override string ErrorCode => "HouseGroupMemberIsNotOwner";

        [Order]
        public string PersonId { get; }

        public HouseGroupMemberIsNotOwnerException(string personId) : base($"House group member with person id '{personId}' is not owner.")
        {
            PersonId = personId;
        }

    }
}
