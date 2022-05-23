using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Models;

namespace SharedHome.Infrastructure.Identity.Auth
{
    public interface IAuthManager
    {
        AuthenticationSucessResult CreateToken(string userId, string email, IEnumerable<string> roles);
    }
}
