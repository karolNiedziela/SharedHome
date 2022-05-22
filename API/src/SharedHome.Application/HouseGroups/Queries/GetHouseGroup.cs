using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.HouseGroups.Queries
{
    public class GetHouseGroup : IAuthorizeRequest, IQuery<Response<HouseGroupDto>>
    {
        public string? PersonId { get; set; }
    }
}
