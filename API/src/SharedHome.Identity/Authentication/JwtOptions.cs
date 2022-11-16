namespace SharedHome.Identity.Authentication
{
    public class JwtOptions
    {
        public string Secret { get; init; } = default!;

        public TimeSpan Expiry { get; init; } = default!;

        public string Issuer { get; init; } = default!;

        public string Audience { get; init; } = default!;
    }
}
