using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Application.Authentication.Commands.ConfirmEmail;
using SharedHome.Application.Authentication.Commands.Register;
using SharedHome.Application.Authentication.Commands.UploadProfileImage;
using SharedHome.Application.Authentication.Models;
using SharedHome.Application.Authentication.Queries.Login;
using SharedHome.Application.Identity.Dto;
using SharedHome.Application.Identity.Queries.GetProfileImage;
using SharedHome.Identity.Authentication;

namespace SharedHome.Api.Controllers
{

    public class IdentityController : ApiController
    {
        /// <summary>
        /// Register new user
        /// </summary>
        [HttpPost(ApiRoutes.Identity.Register)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = new RegisterCommand(request.Email, request.FirstName, request.LastName, request.Password);
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns>Authentication result</returns>
        [HttpPost(ApiRoutes.Identity.Login)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var authenticationResult = await Mediator.Send(query);

            return Ok(authenticationResult);
        }

        /// <summary>
        /// Confirm email address
        /// </summary>
        [HttpGet(ApiRoutes.Identity.ConfirmEmail)]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string code)
        {
            if (code is null || string.IsNullOrWhiteSpace(code) || email is null || string.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            var command = new ConfirmEmailCommand(email, code);
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut(ApiRoutes.Identity.UploadProfileImage)]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProfileImageDto>> UploadProfileImageAsync([FromForm] UploadProfileImageCommand command)
        {
            var profileImage = await Mediator.Send(command);

            return Ok(profileImage);
        }

        [HttpGet(ApiRoutes.Identity.GetProfileImage)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProfileImageDto>> GetProfileImage()
        {
            var profileImage = await Mediator.Send(new GetProfileImage());

            return Ok(profileImage);
        }
    }
}
