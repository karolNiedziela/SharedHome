using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.HouseGroups.Commands
{
    public class AddHouseGroup : AuthorizeRequest, ICommand<Unit>
    {

    }

}
