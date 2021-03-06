using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Application.Invitations.Commands;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{
    public class InvitationsController : ApiController
    {
        /// <summary>
        /// Returns invitation by house group id
        /// </summary>
        /// <returns>Invitation</returns>
        [HttpGet(ApiRoutes.Invitations.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<InvitationDto>>> GetAsync(int houseGroupId)
        {
            var invitation = await Mediator.Send(new GetInvitation
            {
                HouseGroupId = houseGroupId,
            });

            if (invitation.Data is null)
            {
                return NotFound();
            }

            return Ok(invitation);
        }

        /// <summary>
        /// Returns invitations by status
        /// </summary>
        /// <returns>Invitations with given status</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<List<InvitationDto>>>> GetByStatusAsync([FromQuery] GetInvitationsByStatus query)
        {
            var invitations = await Mediator.Send(query);

            return Ok(invitations);
        }

        /// <summary>
        /// Create new invitation
        /// </summary>
        /// <returns>A newly created invitation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendInvitationAsync([FromBody] SendInvitation command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetAsync), new { houseGroupId = response.Data.HouseGroupId }, response);
        }


        /// <summary>
        /// Accept invitation
        /// </summary>
        [HttpPatch(ApiRoutes.Invitations.Accept)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AcceptInvitationAsync([FromBody] AcceptInvitation command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Reject invitation
        /// </summary>
        [HttpPatch(ApiRoutes.Invitations.Reject)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectInvitationAsync([FromBody] RejectInvitation command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Delete shopping list
        /// </summary>
        [HttpDelete(ApiRoutes.Invitations.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteInvitationAsync([FromBody] DeleteInvitation command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
