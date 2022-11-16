using SharedHome.Application.Invitations.Dto;
using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitationsByStatus : AuthorizeRequest, IRequest<Response<List<InvitationDto>>>
    {
        public int? Status { get; set; }
    }
}
