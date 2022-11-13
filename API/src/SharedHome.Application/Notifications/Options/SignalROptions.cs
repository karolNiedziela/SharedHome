namespace SharedHome.Application.Notifications.Options
{
    public class SignalROptions
    {
        public const string SectionName = "SignalR";

        public string NotificationsUri { get; set; } = default!;

        public string NotificationsPattern { get; set; } = default!;
    }
}
