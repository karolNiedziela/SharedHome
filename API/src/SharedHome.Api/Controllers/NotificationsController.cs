using Microsoft.AspNetCore.Mvc;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Queries;

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
        
    }
}
