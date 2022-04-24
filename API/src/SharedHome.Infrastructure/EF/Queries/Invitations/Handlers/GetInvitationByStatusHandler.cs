using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Helpers;

namespace SharedHome.Infrastructure.EF.Queries.Invitations.Handlers
{
    public class GetInvitationByStatusHandler : IQueryHandler<GetInvitationsByStatus, Response<List<InvitationDto>>>
    {
        private readonly SharedHomeDbContext _context;
        private readonly IMapper _mapper;

        public GetInvitationByStatusHandler(SharedHomeDbContext context, IMapper mapper)
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
                .Where(invitation => invitation.Status == invitationStatus &&
                invitation.PersonId == request.PersonId)
                .ToListAsync();

            return new Response<List<InvitationDto>>(_mapper.Map<List<InvitationDto>>(invitations));
        }
    }
}
