using MediatR;
using SharedHome.Application.Invitations.Dto;

using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Invitations.Commands.SendInvitation
{
    public class SendInvitationCommand : AuthorizeRequest, IRequest<Response<InvitationDto>>
    {
        public Guid InvitationId { get; init; } = Guid.NewGuid();

        public string RequestedToPersonEmail { get; set; } = default!;

        public Guid HouseGroupId { get; set; }
    }
}
