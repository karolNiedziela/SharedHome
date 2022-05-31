namespace SharedHome.Infrastructure.Identity.Models
{
    public class JwtDto
    {
        public string AccessToken { get; set; } = default!;

        public long Expiry { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public string Email { get; set; } = default!;

        public IEnumerable<string> Roles { get; set; } = default!;

        public Dictionary<string, IEnumerable<string>> Claims { get; set; } = new();
    }
}
