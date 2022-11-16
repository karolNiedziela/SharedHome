using SharedHome.Application.HouseGroups.DTO;
using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.HouseGroups.Queries
{
    public class GetHouseGroup : AuthorizeRequest, IRequest<Response<HouseGroupDto>>
    {
    }
}
