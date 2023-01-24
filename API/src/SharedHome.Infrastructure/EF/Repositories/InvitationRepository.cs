using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Invitations;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly WriteSharedHomeDbContext _dbContext;

        public InvitationRepository(WriteSharedHomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Invitation?> GetAsync(HouseGroupId houseGroupId, PersonId personId)
            => await _dbContext.Invitations.SingleOrDefaultAsync(i => i.HouseGroupId == houseGroupId &&
            i.RequestedToPersonId == personId);

        public async Task<IEnumerable<Invitation>> GetAllAsync(HouseGroupId houseGroupId)
            => await _dbContext.Invitations.Where(invitation => invitation.HouseGroupId == houseGroupId).ToListAsync();

        public async Task AddAsync(Invitation invitation)
        {
            await _dbContext.AddAsync(invitation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Invitation invitation)
        {
            _dbContext.Invitations.Remove(invitation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<Invitation> invitations)
        {
            _dbContext.Invitations.RemoveRange(invitations);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Invitation invitation)
        {
            _dbContext.Update(invitation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsAnyInvitationFromHouseGroupToPersonAsync(HouseGroupId houseGroupId, PersonId requestedToPersonId)
            => await _dbContext.Invitations.AnyAsync(invitation =>
            invitation.HouseGroupId == houseGroupId &&
            invitation.RequestedToPersonId == requestedToPersonId);
    }
}
