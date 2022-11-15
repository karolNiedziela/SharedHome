using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Invitations.Commands.RejectInvitation
{
    public class RejectInvitationCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid HouseGroupId { get; set; }
    }
}
