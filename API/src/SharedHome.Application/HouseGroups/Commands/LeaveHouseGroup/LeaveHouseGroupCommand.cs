using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.HouseGroups.Commands.LeaveHouseGroup
{
    public class LeaveHouseGroupCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid HouseGroupId { get; set; }
    }
}
