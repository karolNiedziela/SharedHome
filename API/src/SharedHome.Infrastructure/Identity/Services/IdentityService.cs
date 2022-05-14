using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SharedHome.Application.Identity;
using SharedHome.Application.Identity.Models;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Exceptions;
using SharedHome.Shared.Abstractions.Auth;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Abstractions.Exceptions;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Email;
using System.Text;

namespace SharedHome.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthManager _authManager;
        private readonly IIdentityEmailSender _emailSender;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IPersonRepository _personRepository;


        public IdentityService(UserManager<ApplicationUser> userManager, IAuthManager authManager, IIdentityEmailSender emailSender,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, IAuthorizationService authorizationService, IPersonRepository personRepository)
        {
            _userManager = userManager;
            _authManager = authManager;
            _emailSender = emailSender;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _personRepository = personRepository;
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

            // TODO: Assign role

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

            var authenticationResult = _authManager.CreateToken(user.Id, user.FirstName, user.LastName, user.Email);

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

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user is null)
            {
                return false;
            }

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user is null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
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
