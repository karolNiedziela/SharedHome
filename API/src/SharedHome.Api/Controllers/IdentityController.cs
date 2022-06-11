using Microsoft.AspNetCore.Mvc;
using SharedHome.Infrastructure.Identity.Models;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{

    public class IdentityController : ApiController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _identityService.RegisterAsync(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginRequest request)
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
    }
}
