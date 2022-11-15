using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Application.Common.Queries;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.HouseGroups.Queries
{
    public class GetHouseGroup : AuthorizeRequest, IQuery<Response<HouseGroupDto>>
    {
    }
}
