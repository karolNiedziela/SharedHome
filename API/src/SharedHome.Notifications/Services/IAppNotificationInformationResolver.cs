using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Services
{
    public interface IAppNotificationInformationResolver
    {
        string GetTitle(AppNotification appNotification, string? name = null);

        string GetMessage();
    }
}
