using SharedHome.Application.Invitations.Dto;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitation : IAuthorizeRequest, IQuery<Response<InvitationDto>>
    {
        public int HouseGroupId { get; set; }

        public string? PersonId { get; set; }
    }
}
