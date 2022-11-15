using MediatR;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Commands.SendInvitation
{
    public class SendInvitationCommand : AuthorizeRequest, ICommand<Response<InvitationDto>>
    {
        public Guid InvitationId { get; init; } = Guid.NewGuid();

        public string RequestedToPersonEmail { get; set; } = default!;

        public Guid HouseGroupId { get; set; }
    }
}
