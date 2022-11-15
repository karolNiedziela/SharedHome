using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Entities;
using SharedHome.Identity.Exceptions;
using SharedHome.Shared.Abstractions.Authentication;
using SharedHome.Application.Common.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Identity.Queries.Login
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, AuthenticationResponse>
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

            var userRoles = await _userManager.GetRolesAsync(user);

            var authenticationResponse = _authManager.Authenticate(user.Id, user.FirstName, user.LastName, user.Email, userRoles);

            return authenticationResponse;
        }
    }
}
