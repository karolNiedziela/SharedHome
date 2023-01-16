using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Application.HouseGroups.Commands.AddHouseGroup;
using SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup;
using SharedHome.Application.HouseGroups.Commands.HandOwnerRoleOver;
using SharedHome.Application.HouseGroups.Commands.LeaveHouseGroup;
using SharedHome.Application.HouseGroups.Commands.RemoveHouseGroupMember;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Application.HouseGroups.Queries;
using SharedHome.Shared.Application.Responses;

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
        public async Task<ActionResult<ApiResponse<HouseGroupDto>>> GetAsync()
        {
            var houseGroup = await Mediator.Send(new GetHouseGroup());

            return Ok(houseGroup);
        }

        /// <summary>
        /// Create new house group
        /// </summary>
        /// <returns>A newly created house group</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddHouseGroupAsync([FromBody] AddHouseGroupCommand command)
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
        public async Task<IActionResult> RemoveHouseGroupMemberAsync([FromBody] RemoveHouseGroupMemberCommand command)
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
        public async Task<IActionResult> HandOwnerRoleOverAsync([FromBody] HandOwnerRoleOverCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete(ApiRoutes.HouseGroups.Leave)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LeaveHouseGroupAsync([FromBody] LeaveHouseGroupCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete(ApiRoutes.HouseGroups.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteHouseGroupAsync(Guid houseGroupId)
        {
            var command = new DeleteHouseGroupCommand
            {
                HouseGroupId = houseGroupId
            };
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
