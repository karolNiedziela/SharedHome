namespace SharedHome.Shared.Email.Options
{
    public class SendGridOptions
    {
        public string ApiKey { get; set; } = default!;

        public string DefaultFrom { get; set; } = default!;

        public string DefaultFromName { get; set; } = default!;
    }
}
