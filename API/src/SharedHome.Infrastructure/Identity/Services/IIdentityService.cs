using SharedHome.Infrastructure.Authentication;
using SharedHome.Infrastructure.Identity.Models;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.Identity.Services
{
    public interface IIdentityService
    {
        Task<Response<string>> RegisterAsync(RegisterRequest request);

        Task<AuthenticationResponse> LoginAsync(LoginRequest request);

        Task ConfirmEmailAsync(string code, string email);
    }
}
