using Microsoft.AspNetCore.Identity;
using NSubstitute;
using SharedHome.Infrastructure.Identity.Auth;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Exceptions;
using SharedHome.Infrastructure.Identity.Models;
using SharedHome.Infrastructure.Identity.Repositories;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Abstractions.User;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Infrastructure.UnitTests.Identity.Services
{
    public class RefreshTokenServiceTests
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUser _currentUser;
        private readonly IAuthManager _authManager;
        private readonly ITimeProvider _timeProvider;
        private readonly IPasswordHashService _passwordHashService;
        private readonly AuthOptions _authOptions;
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshTokenServiceTests()
        {
            _refreshTokenRepository = Substitute.For<IRefreshTokenRepository>();
            var userStoreSubstitute = Substitute.For<IUserStore<ApplicationUser>>();
            _userManager = Substitute.For<UserManager<ApplicationUser>>(userStoreSubstitute, null, null, null, null, null, null, null, null);
            _currentUser = Substitute.For<ICurrentUser>();
            _authManager = Substitute.For<IAuthManager>();
            _timeProvider = Substitute.For<ITimeProvider>();
            _passwordHashService = Substitute.For<IPasswordHashService>();
            _authOptions = new AuthOptions
            {
                Secret = "secret",
                Expiry = TimeSpan.FromMinutes(10)
            };
            _refreshTokenService = new RefreshTokenService(_refreshTokenRepository, _userManager, _currentUser,
                _authManager, _timeProvider, _passwordHashService, _authOptions);
        }

        [Fact]
        public async Task CreateRefreshToken_Shoudl_Returns_String_Token_And_Call_Repository()
        {
            _passwordHashService.GetSecureSalt().Returns(new byte[] { 0 });

            _passwordHashService.HashUsingPbkdf2(Arg.Any<string>(), Arg.Any<byte[]>()).Returns("tokenHash");

            _timeProvider.CurrentDate().Returns(new DateTime(2022, 10, 10));

            var refreshToken = await _refreshTokenService.CreateRefreshToken("userId");

            await _refreshTokenRepository.Received(1).AddAsync(Arg.Any<RefreshToken>());

            refreshToken.ShouldNotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task RefreshTokenAsync_Throws_InvalidRefreshTokenException_When_RefreshTokenRequest_IsNullOrEmpty(string refreshToken)
        {
            var refreshTokenRequest = new RefreshTokenRequest
            {
                RefreshToken = refreshToken,
            };

            var exception = await Record.ExceptionAsync(() => _refreshTokenService.RefreshTokenAsync(refreshTokenRequest));

            exception.ShouldBeOfType<InvalidRefreshTokenException>();
        }

        [Fact]
        public async Task RefreshTokenAsync_Throws_InvalidRefreshTokenException_When_RefreshToken_IsNull()
        {
            var refreshTokenRequest = new RefreshTokenRequest
            {
                RefreshToken = "refreshToken",
            };

            var exception = await Record.ExceptionAsync(() => _refreshTokenService.RefreshTokenAsync(refreshTokenRequest));

            exception.ShouldBeOfType<InvalidRefreshTokenException>();
        }

        [Fact]
        public async Task RefreshTokenAsync_Throws_InvalidRefreshTokenException_When_TokenHash_Is_Different_From_Generated_Hash()
        {
            var refreshTokenRequest = new RefreshTokenRequest
            {
                RefreshToken = "refreshToken",
            };

            _refreshTokenRepository.GetAsync(Arg.Any<string>()).Returns(new RefreshToken
            (
                "tokenHash",
                "tokenSalt",
                DateTime.Now,
                DateTime.Now,
                "userId"
            ));

            _passwordHashService.HashUsingPbkdf2(Arg.Any<string>(), Arg.Any<byte[]>()).Returns("hash");

            var exception = await Record.ExceptionAsync(() => _refreshTokenService.RefreshTokenAsync(refreshTokenRequest));

            exception.ShouldBeOfType<InvalidRefreshTokenException>();
        }

        [Fact]
        public async Task RefreshTokenAsync_Throws_RefreshTokenExpiredException_When_Token_Expired()
        {
            var refreshTokenRequest = new RefreshTokenRequest
            {
                RefreshToken = "refreshToken",
            };

            _refreshTokenRepository.GetAsync(Arg.Any<string>()).Returns(new RefreshToken
            (
                "tokenHash",
                "tokenSalt",
                DateTime.Now,
                new DateTime(2022, 10, 10),
                "userId"
            ));

            _passwordHashService.HashUsingPbkdf2(Arg.Any<string>(), Arg.Any<byte[]>()).Returns("tokenHash");

            _timeProvider.CurrentDate().Returns(new DateTime(2023, 10, 10));

            var exception = await Record.ExceptionAsync(() => _refreshTokenService.RefreshTokenAsync(refreshTokenRequest));

            exception.ShouldBeOfType<RefreshTokenExpiredException>();
        }

        [Fact]
        public async Task RefreshTokenAsync_Throws_UserNotFoundException_When_User_With_Given_Id_Was_Not_Found()
        {
            var refreshTokenRequest = new RefreshTokenRequest
            {
                RefreshToken = "refreshToken",
            };

            _refreshTokenRepository.GetAsync(Arg.Any<string>()).Returns(new RefreshToken
            (
                "tokenHash",
                "tokenSalt",
                DateTime.Now,
                new DateTime(2022, 10, 10),
                "userId"
            ));

            _passwordHashService.HashUsingPbkdf2(Arg.Any<string>(), Arg.Any<byte[]>()).Returns("tokenHash");

            _timeProvider.CurrentDate().Returns(new DateTime(2021, 10, 10));

            var exception = await Record.ExceptionAsync(() => _refreshTokenService.RefreshTokenAsync(refreshTokenRequest));

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        [Fact]
        public async Task RefreshTokenAsync_Should_Return_AuthenticationSuccessResult()
        {
            var refreshTokenRequest = new RefreshTokenRequest
            {
                RefreshToken = "refreshToken",
            };

            _refreshTokenRepository.GetAsync(Arg.Any<string>()).Returns(new RefreshToken
            (
                "tokenHash",
                "tokenSalt",
                DateTime.Now,
                new DateTime(2022, 10, 10),
                "userId"
            ));

            _currentUser.UserId.Returns("userId");

            _passwordHashService.GetSecureSalt().Returns(new byte []{ 0 });

            _passwordHashService.HashUsingPbkdf2(Arg.Any<string>(), Arg.Any<byte[]>()).Returns("tokenHash");

            _timeProvider.CurrentDate().Returns(new DateTime(2021, 10, 10));

            var applicationUser = new ApplicationUser
            {
                Id = "userId",
                Email = "user@email.com",
            };
            _userManager.FindByIdAsync(Arg.Any<string>()).Returns(applicationUser);

            var roles = new List<string> { "role" };
            _userManager.GetRolesAsync(Arg.Any<ApplicationUser>()).Returns(roles);

            _authManager.CreateToken(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>())
                .Returns(new AuthenticationSucessResult
                {
                    AccessToken = "accessToken",
                    Email = applicationUser.Email,
                    Roles = roles,
                    UserId = applicationUser.Id
                });

            var authenticationResult = await _refreshTokenService.RefreshTokenAsync(refreshTokenRequest);

            authenticationResult.AccessToken.ShouldBe("accessToken");
            authenticationResult.RefreshToken.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task RemoveRefreshTokenAsync_Should_Throws_InvalidRefreshTokenException_When_RefreshToken_Is_Null()
        {
            var exception = await Record.ExceptionAsync(() => _refreshTokenService.RemoveRefreshTokenAsync(Arg.Any<string>()));

            exception.ShouldBeOfType<InvalidRefreshTokenException>();
        }

        [Fact]
        public async Task RemoveRefreshTokenAsync_Should_Call_Repository()
        {
            var refreshToken = new RefreshToken(
                 "tokenHash",
                "tokenSalt",
                DateTime.Now,
                new DateTime(2022, 10, 10),
                "userId"
                );

            _refreshTokenRepository.GetAsync(Arg.Any<string>()).Returns(refreshToken);

            await _refreshTokenService.RemoveRefreshTokenAsync("userId");

            await _refreshTokenRepository.Received(1).DeleteAsync(Arg.Any<RefreshToken>());
        }
    }
}
