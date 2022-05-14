using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Application.Services;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.EF.Queries.Invitations.Handlers
{
    public class GetInvitationHandler : IQueryHandler<GetInvitation, Response<InvitationDto>>
    {
        private readonly WriteSharedHomeDbContext _context;
        private readonly IMapper _mapper;        

        public GetInvitationHandler(WriteSharedHomeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<InvitationDto>> Handle(GetInvitation request, CancellationToken cancellationToken)
        {
            var invitation = await _context.Invitations
                .Include(x => x.RequestedToPersonId == request.PersonId!)
                .SingleOrDefaultAsync(invitation => invitation.HouseGroupId == request.HouseGroupId &&
                invitation.RequestedToPersonId == request.PersonId);

            return new Response<InvitationDto>(_mapper.Map<InvitationDto>(invitation));
        }
    }
}
