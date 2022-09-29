using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Hubs;
using SharedHome.Notifications.Services;
using SharedHome.Shared.Abstractions.User;

namespace SharedHome.Api.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly IAppNotificationService _appNotificationService;
        private readonly ICurrentUser _currentUser;

        public NotificationsController(IAppNotificationService appNotificationService, ICurrentUser currentUser)
        {
            _appNotificationService = appNotificationService;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppNotificationDto>>> GetNotifications()
        {
            var notifications = await _appNotificationService.GetAll(_currentUser.UserId);

            return Ok(notifications);
        }
    }
}
