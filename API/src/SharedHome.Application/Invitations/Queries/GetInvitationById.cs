using MediatR;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitationById : IRequest<Response<InvitationDto>>
    {
        public Guid Id { get; set; }
    }
}
