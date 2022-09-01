namespace SharedHome.Client.Core.Models
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }

        public int Expiry { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; } = new List<string>();

        public Dictionary<string, IEnumerable<string>> Claims { get; set; } = new();
    }
}
