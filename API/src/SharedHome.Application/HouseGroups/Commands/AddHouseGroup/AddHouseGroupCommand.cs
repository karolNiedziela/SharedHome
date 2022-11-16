using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;
using MediatR;

namespace SharedHome.Application.HouseGroups.Commands.AddHouseGroup
{
    public class AddHouseGroupCommand : AuthorizeRequest, IRequest<Response<HouseGroupDto>>
    {
        public Guid HouseGroupId { get; init; } = Guid.NewGuid();

        public string Name { get; set; } = default!;
    }
}
