using SharedHome.Domain.Invitations.Aggregates;

namespace SharedHome.Domain.Invitations.Repositories
{
    public interface IInvitationRepository
    {
        Task<Invitation?> GetAsync(int houseGroupId, string personId);

        Task AddAsync(Invitation invitation);

        Task DeleteAsync(Invitation invitation);

        Task UpdateAsync(Invitation invitation);
    }
}
