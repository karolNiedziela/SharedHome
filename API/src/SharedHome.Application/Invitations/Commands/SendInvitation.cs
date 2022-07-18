using MediatR;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Commands
{
    public class SendInvitation : AuthorizeRequest, ICommand<Response<InvitationDto>>
    {
        public string RequestedToPersonId { get; set; } = default!;

        public int HouseGroupId { get; set; }
    }
}
