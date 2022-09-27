using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Hubs;
using SharedHome.Notifications.Services;

namespace SharedHome.Api.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly IAppNotificationService _appNotificationService;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public NotificationsController(IAppNotificationService appNotificationService, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _appNotificationService = appNotificationService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppNotificationDto>>> GetNotifications()
        {
            var notifications = await _appNotificationService.GetAll("6B0EE0EA-23EF-4A51-AE07-BE9B7FD9FFC6");

            return Ok(notifications);
        }
    }
}
