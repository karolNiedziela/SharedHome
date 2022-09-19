using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup
{
    public class DeleteHouseGroupCommand : AuthorizeRequest, ICommand<Response<Unit>>
    {
        public int HouseGroupId { get; set; }
    }
}
