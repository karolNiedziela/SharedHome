using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedHome.Infrastructure.Identity.Models;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Abstractions.User;

namespace SharedHome.Api.Controllers
{

    public class IdentityController : ApiController
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ICurrentUser _currentUser;

        public IdentityController(IIdentityService identityService, IRefreshTokenService refreshTokenService, ICurrentUser currentUser)
        {
            _identityService = identityService;
            _refreshTokenService = refreshTokenService;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var response = await _identityService.RegisterAsync(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationSucessResult>> Login([FromBody] LoginRequest request)
        {
            var authenticationResult = await _identityService.LoginAsync(request);

            return Ok(authenticationResult);
        }

        [HttpGet]
        [Route("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string code)
        {
            if (code is null || string.IsNullOrWhiteSpace(code) || email is null || string.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            await _identityService.ConfirmEmailAsync(code, email);

            return Ok();
        }

        [HttpPost]
        [Route("refreshtoken")]
        [Authorize]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var authenticationResult = await _refreshTokenService.RefreshTokenAsync(request);

            return Ok(authenticationResult);
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await _identityService.LogoutAsync(_currentUser.UserId);

            return Ok();
        }
    }
}
