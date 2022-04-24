using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.HouseGroups.Commands
{
    public class RemoveHouseGroupMember : AuthorizeRequest, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }

        public string PersonToRemoveId { get; set; } = default!;
    }
}
