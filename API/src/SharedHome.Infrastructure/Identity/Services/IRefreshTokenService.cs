using SharedHome.Infrastructure.Identity.Models;

namespace SharedHome.Infrastructure.Identity.Services
{
    public interface IRefreshTokenService
    {
        Task<string> CreateRefreshToken(string userId);

        Task<AuthenticationSucessResult> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);

        Task RemoveRefreshTokenAsync(string userId);

    }
}
