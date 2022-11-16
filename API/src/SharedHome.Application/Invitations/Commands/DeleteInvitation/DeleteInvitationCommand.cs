using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Invitations.Commands.DeleteInvitation
{
    public class DeleteInvitationCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid HouseGroupId { get; set; }
    }
}
