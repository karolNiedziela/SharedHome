using SharedHome.Application.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.HouseGroups.Queries
{
    public class GetHouseGroup : AuthorizeCommand, IQuery<Response<HouseGroupDto>>
    {

    }
}
