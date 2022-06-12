namespace SharedHome.Shared.Email.Options
{
    public class SendGridSettings
    {
        public const string SectionName = "SendGrid";

        public string ApiKey { get; set; } = default!;

        public string DefaultFromEmailAddress { get; set; } = default!;
    }
}
