using Microsoft.EntityFrameworkCore;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Invitations;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.ReadServices
{
    internal class InvitationReadService : IInvitationReadService
    {
        private readonly DbSet<InvitationReadModel> _invitations;

        public InvitationReadService(ReadSharedHomeDbContext context)
        {
            _invitations = context.Invitations;
        }

        public async Task<InvitationReadModel?> GetByIdAsync(Guid id)
            => await _invitations
            .Include(x => x.RequestedByPerson)
            .Include(x => x.RequestedToPerson)
            .Include(x => x.HouseGroup)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> IsAnyInvitationFromHouseGroupToPersonAsync(Guid houseGroupId, Guid requestedToPersonId)
           => await _invitations.AnyAsync(invitation => 
           invitation.HouseGroupId == houseGroupId &&
           invitation.RequestedToPersonId == requestedToPersonId);
    }
}
