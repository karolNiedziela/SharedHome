﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Infrastructure.Identity.Auth;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Exceptions;
using SharedHome.Infrastructure.Identity.Models;
using SharedHome.Infrastructure.Identity.Repositories;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Abstractions.Exceptions;
using SharedHome.Shared.Abstractions.Responses;
using System.Text;

namespace SharedHome.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthManager _authManager;
        private readonly IIdentityEmailSender _emailSender;        
        private readonly IPersonRepository _personRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityService(UserManager<ApplicationUser> userManager, IAuthManager authManager, IIdentityEmailSender emailSender,
             IPersonRepository personRepository, IRefreshTokenRepository refreshTokenRepository, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _authManager = authManager;
            _emailSender = emailSender;
            _personRepository = personRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Response<string>> RegisterAsync(RegisterUserRequest request)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
            };

            var result = await _userManager.CreateAsync(applicationUser, request.Password);

            await _userManager.AddToRoleAsync(applicationUser, AppIdentityConstants.Roles.User);

            if (!result.Succeeded)
            {
                throw new IdentityException(MapIdentityErrorToIEnumerableString(result.Errors));
            }

            var person = Person.Create(applicationUser.Id, applicationUser.FirstName, applicationUser.LastName);
            await _personRepository.AddAsync(person);

            if (_userManager.Options.SignIn.RequireConfirmedEmail)
            {
                await SendConfirmationEmailAsync(applicationUser); 

                return new Response<string>("Thanks for signing up. Check your mailbox and confirm email to get fully access.");
            }

            return new Response<string>("Thanks for signing up.");
        }

        public async Task<AuthenticationSucessResult> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                throw new InvalidCredentialsException();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authenticationResult = _authManager.CreateToken(user.Id, user.Email, userRoles);
            var refreshToken = await _refreshTokenService.CreateRefreshToken(user.Id);
            authenticationResult.RefreshToken = refreshToken;

            return authenticationResult;
        }

        public async Task ConfirmEmailAsync(string code, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            var token = Decode(code);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new IdentityException(MapIdentityErrorToIEnumerableString(result.Errors));
            }
        }

        public async Task<Response<string>> LogoutAsync(string userId)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(userId);
            if (refreshToken is null)
            {
                throw new InvalidRefreshTokenException();
            }

            await _refreshTokenService.RemoveRefreshTokenAsync(userId);

            return new Response<string>("Logout successfully.");
        }

        private async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var code = Encode(token);

            await _emailSender.SendAsync(user.Email, code);
        }

        private static IEnumerable<string> MapIdentityErrorToIEnumerableString(IEnumerable<IdentityError> errors)
            => errors.Select(error => error.Description);

        private static string Encode(string token)
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        private static string Decode(string code)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }        
    }
}
