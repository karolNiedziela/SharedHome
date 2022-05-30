using SharedHome.Infrastructure.Identity.Models;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.Identity.Services
{
    public interface IIdentityService
    {
        Task<Response<string>> RegisterAsync(RegisterUserRequest request);

        Task<AuthenticationSucessResult> LoginAsync(LoginRequest request);

        Task ConfirmEmailAsync(string code, string email);
    }
}
