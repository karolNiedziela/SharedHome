using MediatR;

using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup
{
    public class DeleteHouseGroupCommand : AuthorizeRequest, IRequest<ApiResponse<Unit>>
    {
        public Guid HouseGroupId { get; set; }
    }
}
