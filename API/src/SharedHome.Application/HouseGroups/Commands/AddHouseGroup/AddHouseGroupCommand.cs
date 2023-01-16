using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;
using MediatR;

namespace SharedHome.Application.HouseGroups.Commands.AddHouseGroup
{
    public class AddHouseGroupCommand : AuthorizeRequest, IRequest<ApiResponse<HouseGroupDto>>
    {
        public string Name { get; set; } = default!;
    }
}
