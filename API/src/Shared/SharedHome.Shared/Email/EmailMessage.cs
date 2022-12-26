using MimeKit;

namespace SharedHome.Shared.Email
{
    public class EmailMessage
    {
        public List<MailboxAddress> Recipients { get; set; } = new();

        public string Subject { get; set; } = default!;

        public string Body { get; set; } = default!;

        public string? From { get; set; }

        // Key: Links, Value: LinkText
        public Dictionary<string, string> Links { get; set; } = new();
    }
}
