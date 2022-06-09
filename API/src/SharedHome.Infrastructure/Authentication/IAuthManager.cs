namespace SharedHome.Infrastructure.Authentication
{
    public interface IAuthManager
    {
        AuthenticationResponse CreateToken(string userId, string email, IEnumerable<string> roles);
    }
}
