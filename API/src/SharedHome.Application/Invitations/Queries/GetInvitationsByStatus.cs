using SharedHome.Application.Invitations.Dto;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitationsByStatus : AuthorizeRequest, IQuery<Response<List<InvitationDto>>>
    {
        public int? Status { get; set; }
    }
}
