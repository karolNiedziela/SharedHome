using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Common.Queries;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitation : AuthorizeRequest, IQuery<Response<InvitationDto>>
    {
        public Guid HouseGroupId { get; set; }
    }
}
