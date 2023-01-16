using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using MediatR;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Infrastructure.EF.Queries.Invitations.Handlers
{
    internal class GetInvitationByHouseGroupIdHandler : IRequestHandler<GetInvitationByHouseGroupId, ApiResponse<InvitationDto>>
    {
        private readonly DbSet<InvitationReadModel> _invitations;
        private readonly IMapper _mapper;        

        public GetInvitationByHouseGroupIdHandler(ReadSharedHomeDbContext context, IMapper mapper)
        {
            _invitations = context.Invitations;
            _mapper = mapper;
        }

        public async Task<ApiResponse<InvitationDto>> Handle(GetInvitationByHouseGroupId request, CancellationToken cancellationToken)
        {
            var invitation = await _invitations
                .Include(x => x.RequestedToPersonId == request.PersonId!)
                .SingleOrDefaultAsync(invitation => invitation.HouseGroupId == request.HouseGroupId &&
                invitation.RequestedToPersonId == request.PersonId);

            return new ApiResponse<InvitationDto>(_mapper.Map<InvitationDto>(invitation!));
        }
    }
}
