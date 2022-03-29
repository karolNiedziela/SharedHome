using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.Invitations.Commands;

namespace SharedHome.Api.Controllers
{
    public class InvitationsController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> SendInvitationAsync([FromBody] SendInvitation command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("accept")]
        public async Task<IActionResult> AcceptInvitationAsync([FromBody] AcceptInvitation command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("reject")]
        public async Task<IActionResult> RejectInvitationAsync([FromBody] RejectInvitation command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("houseGroupId:int")]
        public async Task<IActionResult> DeleteInvitationAsync([FromBody] DeleteInvitation command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
