namespace SharedHome.Application.Notifications
{
    public class SignalRSettings
    {
        public const string SectionName = "SignalR";

        public string NotificationsUri { get; set; } = default!;

        public string NotificationsPattern { get; set; } = default!;
    }
}
