using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.DTO;
using SharedHome.Application.HouseGroups.Commands;
using SharedHome.Application.HouseGroups.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{
    public class HouseGroupsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<Response<HouseGroupDto>>> GetAsync()
        {
            var houseGroup = await Mediator.Send(new GetHouseGroup());

            if (houseGroup.Data is null)
            {
                return NotFound();
            }

            return Ok(houseGroup);
        }

        [HttpPost]
        public async Task<IActionResult> AddHouseGroupAsync([FromBody] AddHouseGroup command)
        {
            await Mediator.Send(command);

            return Created("", new { });
        }

        [HttpDelete("{houseGroupId:int}/members/{personToRemoveId:guid}")]
        public async Task<IActionResult> RemoveHouseGroupMemberAsync([FromBody] RemoveHouseGroupMember command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{houseGroupId:int}/handownerroleover")]
        public async Task<IActionResult> HandOwnerRoleOverAsync([FromBody] HandOwnerRoleOver command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
