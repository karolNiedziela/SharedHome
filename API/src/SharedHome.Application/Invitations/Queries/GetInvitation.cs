using SharedHome.Application.Invitations.Dto;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitation : AuthorizeRequest, IQuery<Response<InvitationDto>>
    {
        public Guid HouseGroupId { get; set; }
    }
}
