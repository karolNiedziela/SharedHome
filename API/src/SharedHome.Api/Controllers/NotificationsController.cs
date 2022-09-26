using Microsoft.AspNetCore.Mvc;
using SharedHome.Notifications.Services;

namespace SharedHome.Api.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly IAppNotificationService _appNotificationService;

        public NotificationsController(IAppNotificationService appNotificationService)
        {
            _appNotificationService = appNotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications = await _appNotificationService.GetAll("6B0EE0EA-23EF-4A51-AE07-BE9B7FD9FFC6");

            return Ok(notifications);
        }
    }
}
