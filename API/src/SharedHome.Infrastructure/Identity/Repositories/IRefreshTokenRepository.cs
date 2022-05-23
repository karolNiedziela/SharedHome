using SharedHome.Infrastructure.Identity.Entities;

namespace SharedHome.Infrastructure.Identity.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetAsync(string userId);

        Task AddAsync(RefreshToken refreshToken);

        Task UpdateAsync(RefreshToken refreshToken);

        Task DeleteAsync(RefreshToken refreshToken);
    }
}
