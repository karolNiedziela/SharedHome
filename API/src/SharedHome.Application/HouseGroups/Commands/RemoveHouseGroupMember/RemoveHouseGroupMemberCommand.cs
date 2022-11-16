using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.HouseGroups.Commands.RemoveHouseGroupMember
{
    public class RemoveHouseGroupMemberCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid HouseGroupId { get; set; }

        public Guid PersonToRemoveId { get; set; } = default!;
    }
}
