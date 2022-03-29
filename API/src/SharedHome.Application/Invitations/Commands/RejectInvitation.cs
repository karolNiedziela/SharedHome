using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Invitations.Commands
{
    public class RejectInvitation : AuthorizeCommand, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }
    }
}
