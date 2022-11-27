using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Application.Authentication.Commands.ConfirmEmail;
using SharedHome.Application.Authentication.Commands.UploadProfileImage;
using SharedHome.Application.Authentication.Models;
using SharedHome.Application.Authentication.Queries.Login;
using SharedHome.Application.Identity.Commands.ChangePassword;
using SharedHome.Application.Identity.Commands.ForgotPassword;
using SharedHome.Application.Identity.Commands.Register;
using SharedHome.Application.Identity.Commands.ResetPassword;
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
        [HttpPost(ApiRoutes.Identity.ConfirmEmail)]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Code) || string.IsNullOrWhiteSpace(command.Email))
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Reset password
        /// </summary>
        [HttpPost(ApiRoutes.Identity.ResetPassword)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Code))
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Forgot password
        /// </summary>
        [HttpPost(ApiRoutes.Identity.ForgotPassword)]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Change password
        /// </summary>
        [HttpPut(ApiRoutes.Identity.ChangePassword)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ChangePasswordCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
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
