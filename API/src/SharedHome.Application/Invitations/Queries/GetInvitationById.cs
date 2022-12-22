using MediatR;
using SharedHome.Application.Invitations.Dto;

namespace SharedHome.Application.Invitations.Queries
{
    public class GetInvitationById : IRequest<InvitationDto>
    {
        public Guid Id { get; set; }
    }
}
