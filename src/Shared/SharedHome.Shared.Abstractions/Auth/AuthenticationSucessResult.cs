namespace SharedHome.Shared.Abstractions.Auth
{
    public class AuthenticationSucessResult
    {
        public string AccessToken { get; set; } = default!;

        public long Expiry { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public string Role { get; set; } = default!;

        public string Email { get; set; } = default!;

        public Dictionary<string, IEnumerable<string>> Claims { get; set; } = new();
    }
}
