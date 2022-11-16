using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Invitations.Commands.AcceptInvitation
{
    public class AcceptInvitationCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid HouseGroupId { get; set; }
    }
}
