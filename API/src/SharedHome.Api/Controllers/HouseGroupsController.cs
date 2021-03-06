using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Application.HouseGroups.Commands;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Application.HouseGroups.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{
    public class HouseGroupsController : ApiController
    {
        /// <summary>
        /// Returns house group for person
        /// </summary>
        /// <returns>House group</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<HouseGroupDto>>> GetAsync()
        {
            var houseGroup = await Mediator.Send(new GetHouseGroup());

            if (houseGroup.Data is null)
            {
                return NotFound();
            }

            return Ok(houseGroup);
        }

        /// <summary>
        /// Create new house group
        /// </summary>
        /// <returns>A newly created house group</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddHouseGroupAsync([FromBody] AddHouseGroup command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetAsync), new { }, response);
        }

        /// <summary>
        /// Delete  house group
        /// </summary>
        [HttpDelete(ApiRoutes.HouseGroups.RemoveMember)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveHouseGroupMemberAsync([FromBody] RemoveHouseGroupMember command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Change owner of house group
        /// </summary>
        [HttpPatch(ApiRoutes.HouseGroups.HandOwnerRoleOver)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> HandOwnerRoleOverAsync([FromBody] HandOwnerRoleOver command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
