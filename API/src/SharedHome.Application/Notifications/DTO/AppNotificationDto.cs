namespace SharedHome.Notifications.DTO
{
    public class AppNotificationDto
    {
        public string Title { get; set; } = default!;

        public bool IsRead { get; set; }

        public string? Type { get; set; }

        public string CreatedByFullName { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = default!;
    }
}
