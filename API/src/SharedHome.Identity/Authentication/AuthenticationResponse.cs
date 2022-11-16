namespace SharedHome.Identity.Authentication
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; } = default!;

        public long Expiry { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public IEnumerable<string> Roles { get; set; } = default!;

        public Dictionary<string, IEnumerable<string>> Claims { get; set; } = new();
    }
}
