using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Domain.Invitations.Enums;
using SharedHome.Infrastructure.EF.Contexts;
using MediatR;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Helpers;

namespace SharedHome.Infrastructure.EF.Queries.Invitations.Handlers
{
    internal class GetInvitationByStatusHandler : IRequestHandler<GetInvitationsByStatus, Response<List<InvitationDto>>>
    {
        private readonly ReadSharedHomeDbContext _context;
        private readonly IMapper _mapper;

        public GetInvitationByStatusHandler(ReadSharedHomeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<InvitationDto>>> Handle(GetInvitationsByStatus request, CancellationToken cancellationToken)
        {
            var invitationStatus = request.Status.HasValue ?
                EnumHelper.ToEnumByIntOrThrow<InvitationStatus>(request.Status.Value) :
                InvitationStatus.Pending;

            var invitations = await _context.Invitations
                .Include(x => x.RequestedByPerson)
                .Include(x => x.RequestedToPerson)
                .Include(x => x.HouseGroup)
                .Where(invitation => invitation.Status == (int)invitationStatus &&
                invitation.RequestedToPersonId == request.PersonId)
                .ToListAsync();

            return new Response<List<InvitationDto>>(_mapper.Map<List<InvitationDto>>(invitations));
        }
    }
}
