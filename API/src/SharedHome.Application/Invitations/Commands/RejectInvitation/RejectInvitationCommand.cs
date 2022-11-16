using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Invitations.Commands.RejectInvitation
{
    public class RejectInvitationCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid HouseGroupId { get; set; }
    }
}
