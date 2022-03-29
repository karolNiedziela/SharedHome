using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Invitations.Commands
{
    public class SendInvitation : AuthorizeCommand, ICommand<Unit>
    {
        public string RequestedToPersonId { get; set; } = default!;

        public int HouseGroupId { get; set; }
    }
}
