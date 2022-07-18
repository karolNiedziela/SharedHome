using MediatR;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.HouseGroups.Commands
{
    public class AddHouseGroup : AuthorizeRequest, ICommand<Response<HouseGroupDto>>
    {
    }

}
