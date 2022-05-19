using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.EF.Queries.Invitations.Handlers
{
    internal class GetInvitationHandler : IQueryHandler<GetInvitation, Response<InvitationDto>>
    {
        private readonly DbSet<InvitationReadModel> _invitations;
        private readonly IMapper _mapper;        

        public GetInvitationHandler(ReadSharedHomeDbContext context, IMapper mapper)
        {
            _invitations = context.Invitations;
            _mapper = mapper;
        }

        public async Task<Response<InvitationDto>> Handle(GetInvitation request, CancellationToken cancellationToken)
        {
            var invitation = await _invitations
                .Include(x => x.RequestedToPersonId == request.PersonId!)
                .SingleOrDefaultAsync(invitation => invitation.HouseGroupId == request.HouseGroupId &&
                invitation.RequestedToPersonId == request.PersonId);

            return new Response<InvitationDto>(_mapper.Map<InvitationDto>(invitation));
        }
    }
}
