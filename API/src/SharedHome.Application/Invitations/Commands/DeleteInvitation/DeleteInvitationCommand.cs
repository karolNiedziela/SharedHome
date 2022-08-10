using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Invitations.Commands.DeleteInvitation
{
    public class DeleteInvitationCommand : AuthorizeRequest, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }
    }
}
