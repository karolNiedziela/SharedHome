using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Services;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly SharedHomeDbContext _dbContext;

        public InvitationService(SharedHomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsAnyInvitationFromHouseGroupToPerson(int houseGroupId, string personId)
           => await _dbContext.Invitations.AnyAsync(invitation => invitation.HouseGroupId == houseGroupId &&
           invitation.PersonId == personId);
    }
}
