using SharedHome.Application.Identity.Models;
using SharedHome.Shared.Abstractions.Auth;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Identity
{
    public interface IIdentityService
    {
        Task<Response<string>> RegisterAsync(RegisterUserRequest request);

        Task<AuthenticationSucessResult> LoginAsync(LoginRequest request);

        Task ConfirmEmailAsync(string code, string email);
    }
}
