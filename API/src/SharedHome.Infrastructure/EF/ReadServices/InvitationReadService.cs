using Microsoft.EntityFrameworkCore;
using SharedHome.Application.ReadServices;
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

        public async Task<bool> IsAnyInvitationFromHouseGroupToPerson(int houseGroupId, string requestedToPersonId)
           => await _invitations.AnyAsync(invitation => invitation.HouseGroupId == houseGroupId &&
           invitation.RequestedToPersonId == requestedToPersonId);
    }
}
