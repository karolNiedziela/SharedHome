using MediatR;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Commands.SendInvitation
{
    public class SendInvitationCommand : AuthorizeRequest, ICommand<Response<InvitationDto>>
    {
        public string RequestedToPersonEmail { get; set; } = default!;

        public int HouseGroupId { get; set; }
    }
}
