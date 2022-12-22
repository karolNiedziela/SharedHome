using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Infrastructure.EF.Queries.Invitations.Handlers
{
    internal class GetInvitationByIdHandler : IRequestHandler<GetInvitationById, InvitationDto>
    {
        private readonly DbSet<InvitationReadModel> _invitations;
        private readonly IMapper _mapper;

        public GetInvitationByIdHandler(ReadSharedHomeDbContext context, IMapper mapper)
        {
            _invitations = context.Invitations;
            _mapper = mapper;
        }

        public async Task<InvitationDto> Handle(GetInvitationById request, CancellationToken cancellationToken)
        {
            var invitation = await _invitations
                .Include(x => x.RequestedByPerson)
                .Include(x => x.RequestedToPerson)
                .Include(x => x.HouseGroup)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<InvitationDto>(invitation!);
        }
    }
}
