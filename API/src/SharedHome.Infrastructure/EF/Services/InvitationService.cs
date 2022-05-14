using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Services;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly WriteSharedHomeDbContext _dbContext;

        public InvitationService(WriteSharedHomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsAnyInvitationFromHouseGroupToPerson(int houseGroupId, string requestedToPersonId)
           => await _dbContext.Invitations.AnyAsync(invitation => invitation.HouseGroupId == houseGroupId &&
           invitation.RequestedToPersonId == requestedToPersonId);
    }
}
