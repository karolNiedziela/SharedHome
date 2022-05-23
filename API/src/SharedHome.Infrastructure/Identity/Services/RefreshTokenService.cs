using Microsoft.AspNetCore.Identity;
using SharedHome.Infrastructure.Identity.Auth;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Exceptions;
using SharedHome.Infrastructure.Identity.Models;
using SharedHome.Infrastructure.Identity.Repositories;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Abstractions.User;
using System.Security.Cryptography;
using System.Text;

namespace SharedHome.Infrastructure.Identity.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUser _currentUser;
        private readonly IAuthManager _authManager;
        private readonly ITimeProvider _time;
        private readonly IPasswordHashService _passwordHashService;
        private readonly AuthOptions _authOptions;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, UserManager<ApplicationUser> userManager, ICurrentUser currentUser, IAuthManager authManager,
            ITimeProvider time, IPasswordHashService passwordHashService, AuthOptions authOptions)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _currentUser = currentUser;
            _authManager = authManager;
            _time = time;
            _passwordHashService = passwordHashService;
            _authOptions = authOptions;
        }

        public async Task<string> CreateRefreshToken(string userId)
        {
            var secureRandomBytes = new byte[32];

            using var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(secureRandomBytes);

            var refreshToken = Convert.ToBase64String(secureRandomBytes);

            var salt = _passwordHashService.GetSecureSalt();
            var refreshTokenHashed = _passwordHashService.HashUsingPbkdf2(refreshToken, salt);

            var now = _time.CurrentDate();

            var refreshTokenObject = new RefreshToken(refreshTokenHashed, Convert.ToBase64String(salt), now, now.AddDays(_authOptions.Expiry.Days - 1), userId);

            await _refreshTokenRepository.AddAsync(refreshTokenObject);

            return refreshToken;
        }

        public async Task<AuthenticationSucessResult> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            await ValidateRefreshTokenAsync(refreshTokenRequest);

            var user = await _userManager.FindByIdAsync(_currentUser.UserId);
            if (user is null)
            {
                throw new UserNotFoundException(_currentUser.UserId);
            }

            await RemoveRefreshTokenAsync(_currentUser.UserId);

            var userRoles = await _userManager.GetRolesAsync(user);
            var authenticationResult = _authManager.CreateToken(_currentUser.UserId, user.Email, userRoles);
            var refreshTokenValue = await CreateRefreshToken(_currentUser.UserId);
            authenticationResult.RefreshToken = refreshTokenValue;

            return authenticationResult;
        }
        public async Task RemoveRefreshTokenAsync(string userId)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(userId);
            if (refreshToken is null)
            {
                throw new InvalidRefreshTokenException();
            }

            await _refreshTokenRepository.DeleteAsync(refreshToken);
        }       

        private async Task ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            if (refreshTokenRequest == null || string.IsNullOrEmpty(refreshTokenRequest.RefreshToken))
            {
                throw new InvalidRefreshTokenException();
            }

            var refreshToken = await _refreshTokenRepository.GetAsync(_currentUser.UserId);
            if (refreshToken is null)
            {
                throw new InvalidRefreshTokenException();
            }

            var refreshTokenValidateHash = _passwordHashService.HashUsingPbkdf2(refreshTokenRequest.RefreshToken, Encoding.ASCII.GetBytes(refreshToken.TokenSalt));

            if (refreshToken.TokenHash != refreshTokenValidateHash)
            {
                throw new InvalidRefreshTokenException();
            }

            if (refreshToken.ExpiryDate < _time.CurrentDate())
            {
                throw new RefreshTokenExpiredException();
            }
        }

    }
}
