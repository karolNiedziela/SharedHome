namespace SharedHome.Identity.Authentication.Services
{
    public interface IAuthManager
    {
        AuthenticationResponse Authenticate(string userId, string firstName, string lastName, string email, IEnumerable<string> roles);
    }
}
