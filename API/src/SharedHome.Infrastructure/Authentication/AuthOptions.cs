namespace SharedHome.Infrastructure.Authentication
{
    public class AuthOptions
    {
        public const string AuthOptionsName = "Auth";

        public string Secret { get; set; } = default!;

        public TimeSpan Expiry { get; set; } = default!;
    }
}
