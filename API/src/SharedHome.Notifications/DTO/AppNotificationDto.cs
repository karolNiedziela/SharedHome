namespace SharedHome.Notifications.DTO
{
    public class AppNotificationDto
    {
        public string Title { get; set; } = default!;

        public string? Message { get; set; }

        public bool IsRead { get; set; }

        public string? Type { get; set; }

        public string Target { get; set; } = default!;

        public string Operation { get; set; } = default!;

    }
}
