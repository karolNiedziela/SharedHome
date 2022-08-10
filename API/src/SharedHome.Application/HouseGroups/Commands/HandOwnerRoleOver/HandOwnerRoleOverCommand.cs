using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.HouseGroups.Commands.HandOwnerRoleOver
{
    public class HandOwnerRoleOverCommand : AuthorizeRequest, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }

        public string NewOwnerPersonId { get; set; } = default!;
    }
}
