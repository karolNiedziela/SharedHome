using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.HouseGroups.Commands.RemoveHouseGroupMember
{
    public class RemoveHouseGroupMemberCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid HouseGroupId { get; set; }

        public Guid PersonToRemoveId { get; set; } = default!;
    }
}
