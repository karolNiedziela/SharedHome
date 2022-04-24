using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.Invitations.Commands;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{
    public class InvitationsController : ApiController
    {
        [HttpGet("{houseGroupId:int}")]
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

        [HttpGet]
        public async Task<ActionResult<Response<List<InvitationDto>>>> GetByStatusAsync([FromQuery] GetInvitationsByStatus query)
        {
            var invitations = await Mediator.Send(query);

            return Ok(invitations);
        }

        [HttpPost]
        public async Task<IActionResult> SendInvitationAsync([FromBody] SendInvitation command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("accept")]
        public async Task<IActionResult> AcceptInvitationAsync([FromBody] AcceptInvitation command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("reject")]
        public async Task<IActionResult> RejectInvitationAsync([FromBody] RejectInvitation command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{houseGroupId:int}")]
        public async Task<IActionResult> DeleteInvitationAsync([FromBody] DeleteInvitation command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
