using SharedHome.Application.Invitations.Dto;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitationsByStatus : IAuthorizeRequest, IQuery<Response<List<InvitationDto>>>
    {
        public int? Status { get; set; }

        public string? PersonId { get; set; }
    }
}
