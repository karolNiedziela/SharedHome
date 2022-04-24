using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Invitations.Commands
{
    public class SendInvitation : AuthorizeRequest, ICommand<Unit>
    {
        public string RequestedToPersonId { get; set; } = default!;

        public int HouseGroupId { get; set; }
    }
}
