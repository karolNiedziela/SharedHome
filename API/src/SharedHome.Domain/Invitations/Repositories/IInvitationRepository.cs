using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.Invitations.Repositories
{
    public interface IInvitationRepository
    {
        Task<Invitation?> GetAsync(HouseGroupId houseGroupId, PersonId personId);

        Task<IEnumerable<Invitation>> GetAllAsync(HouseGroupId houseGroupId);

        Task AddAsync(Invitation invitation);

        Task DeleteAsync(Invitation invitation);

        Task DeleteAsync(IEnumerable<Invitation> invitations);

        Task UpdateAsync(Invitation invitation);

        Task<bool> IsAnyInvitationFromHouseGroupToPersonAsync(HouseGroupId houseGroupId, PersonId requestedToPersonId);
    }
}
