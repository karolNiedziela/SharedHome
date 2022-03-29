using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands
{
    public class HandOwnerRoleOver : AuthorizeCommand, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }

        public string NewOwnerPersonId { get; set; } = default!;
    }
}
