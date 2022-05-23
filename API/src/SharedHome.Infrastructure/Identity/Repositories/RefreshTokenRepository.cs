using Microsoft.EntityFrameworkCore;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.Identity.Entities;

namespace SharedHome.Infrastructure.Identity.Repositories
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentitySharedHomeDbContext _context;

        public RefreshTokenRepository(IdentitySharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetAsync(string userId)
            => await _context.RefreshTokens.FirstOrDefaultAsync(refreshToken => refreshToken.UserId == userId);

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
