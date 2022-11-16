using SharedHome.Application.Invitations.Dto;
using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitation : AuthorizeRequest, IRequest<Response<InvitationDto>>
    {
        public Guid HouseGroupId { get; set; }
    }
}
