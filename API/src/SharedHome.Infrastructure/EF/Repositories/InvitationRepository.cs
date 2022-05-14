using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
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
        public async Task<Invitation?> GetAsync(int houseGroupId, string personId)
            => await _dbContext.Invitations.SingleOrDefaultAsync(i => i.HouseGroupId == houseGroupId &&
            i.RequestedToPersonId == personId);

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

        public async Task UpdateAsync(Invitation invitation)
        {
            _dbContext.Update(invitation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsAnyInvitationFromHouseGroupToPerson(int houseGroupId, string personId)
            => await _dbContext.Invitations.AnyAsync(invitation => invitation.HouseGroupId == houseGroupId &&
            invitation.RequestedToPersonId == personId);
    }
}
