using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.HouseGroups.Commands.HandOwnerRoleOver
{
    public class HandOwnerRoleOverCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid HouseGroupId { get; set; }

        public Guid NewOwnerPersonId { get; set; } = default!;
    }
}
