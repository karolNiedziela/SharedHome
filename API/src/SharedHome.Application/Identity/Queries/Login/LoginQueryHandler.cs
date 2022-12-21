using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Authentication;
using SharedHome.Identity.Authentication.Services;
using SharedHome.Identity.Entities;
using SharedHome.Identity.Exceptions;

namespace SharedHome.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthManager _authManager;

        public LoginQueryHandler(UserManager<ApplicationUser> userManager, IAuthManager authManager)
        {
            _userManager = userManager;
            _authManager = authManager;
        }

        public async Task<AuthenticationResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
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

            if (!user.EmailConfirmed)
            {
                throw new EmailNotConfirmedException();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authenticationResponse = _authManager.Authenticate(user.Id, user.FirstName, user.LastName, user.Email, userRoles);

            return authenticationResponse;
        }
    }
}
