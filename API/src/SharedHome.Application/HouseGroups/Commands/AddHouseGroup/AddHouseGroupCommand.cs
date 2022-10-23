using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.HouseGroups.Commands.AddHouseGroup
{
    public class AddHouseGroupCommand : AuthorizeRequest, ICommand<Response<HouseGroupDto>>
    {
        public Guid HouseGroupId { get; init; } = Guid.NewGuid();

        public string Name { get; set; } = default!;
    }
}
