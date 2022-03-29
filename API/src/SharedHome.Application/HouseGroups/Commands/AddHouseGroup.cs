using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands
{
    public class AddHouseGroup : AuthorizeCommand, ICommand<Unit>
    {

    }

}
