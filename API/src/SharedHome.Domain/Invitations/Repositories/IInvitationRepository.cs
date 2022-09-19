using SharedHome.Domain.Invitations.Aggregates;

namespace SharedHome.Domain.Invitations.Repositories
{
    public interface IInvitationRepository
    {
        Task<Invitation?> GetAsync(int houseGroupId, string personId);

        Task<IEnumerable<Invitation>> GetAllAsync(int houseGroupId);

        Task AddAsync(Invitation invitation);

        Task DeleteAsync(Invitation invitation);

        Task DeleteAsync(IEnumerable<Invitation> invitations);

        Task UpdateAsync(Invitation invitation);
    }
}
