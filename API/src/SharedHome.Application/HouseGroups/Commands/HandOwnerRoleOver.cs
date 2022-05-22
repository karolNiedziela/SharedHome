using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.HouseGroups.Commands
{
    public class HandOwnerRoleOver : IAuthorizeRequest, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }

        public string NewOwnerPersonId { get; set; } = default!;

        public string? PersonId { get; set; }
    }
}
