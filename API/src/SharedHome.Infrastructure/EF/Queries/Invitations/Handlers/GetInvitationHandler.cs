using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.EF.Queries.Invitations.Handlers
{
    public class GetInvitationHandler : IQueryHandler<GetInvitation, Response<InvitationDto>>
    {
        private readonly SharedHomeDbContext _context;
        private readonly IMapper _mapper;

        public GetInvitationHandler(SharedHomeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<InvitationDto>> Handle(GetInvitation request, CancellationToken cancellationToken)
        {
            var invitation = await _context.Invitations
                .SingleOrDefaultAsync(invitation => invitation.HouseGroupId == request.HouseGroupId &&
                invitation.PersonId == request.PersonId);

            return new Response<InvitationDto>(_mapper.Map<InvitationDto>(invitation));
        }
    }
}
