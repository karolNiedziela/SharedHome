using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Infrastructure.Identity.Auth;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Exceptions;
using SharedHome.Infrastructure.Identity.Models;
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
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(UserManager<ApplicationUser> userManager, IAuthManager authManager, IIdentityEmailSender emailSender,
             IPersonRepository personRepository, ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _authManager = authManager;
            _emailSender = emailSender;
            _personRepository = personRepository;            
            _logger = logger;
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
            
            if (!result.Succeeded)
            {
                throw new IdentityException(MapIdentityErrorToIEnumerableString(result.Errors));
            }
            _logger.LogInformation("User with id '{userId}' created.", applicationUser.Id);

            await _userManager.AddToRoleAsync(applicationUser, AppIdentityConstants.Roles.User);

            var person = Person.Create(applicationUser.Id, applicationUser.FirstName, applicationUser.LastName);
            await _personRepository.AddAsync(person);
            _logger.LogInformation("Person with id '{userId}' created.", person.Id);

            if (_userManager.Options.SignIn.RequireConfirmedEmail)
            {
                await SendConfirmationEmailAsync(applicationUser); 

                return new Response<string>("Thanks for signing up. Check your mailbox and confirm email to get fully access.");
            }

            return new Response<string>("Thanks for signing up.");
        }

        public async Task<JwtDto> LoginAsync(LoginRequest request)
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

            _logger.LogInformation("User with id '{userId}' confirmed email.", user.Id);
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
