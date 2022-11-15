using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Invitations.Commands.DeleteInvitation
{
    public class DeleteInvitationCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid HouseGroupId { get; set; }
    }
}
