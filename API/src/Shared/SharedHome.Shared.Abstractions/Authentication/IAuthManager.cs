using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Shared.Abstractions.Authentication
{
    public interface IAuthManager
    {
        AuthenticationResponse Authenticate(string userId, string firstName, string lastName, string email, IEnumerable<string> roles);
    }
}
