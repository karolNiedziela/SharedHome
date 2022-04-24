using SharedHome.Application.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.HouseGroups.Queries
{
    public class GetHouseGroup : AuthorizeRequest, IQuery<Response<HouseGroupDto>>
    {

    }
}
