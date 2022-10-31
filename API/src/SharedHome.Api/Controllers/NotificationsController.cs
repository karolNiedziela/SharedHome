using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Application.Notifications.Commands.MarkNotificationsAsRead;
using SharedHome.Application.Notifications.Queries;
using SharedHome.Notifications.DTO;

namespace SharedHome.Api.Controllers
{
    public class NotificationsController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppNotificationDto>>> GetNotifications()
        {
            var notifications = await Mediator.Send(new GetNotifications());

            return Ok(notifications);
        }

        [HttpPatch(ApiRoutes.Notications.MarkAsRead)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkNotificationsAsReadCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
