using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Entities;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.Identity.Services
{
    public interface IIdentityService
    {
        Task<string> GetEmailConfirmationTokenAsync(ApplicationUser user);

        IEnumerable<string> MapIdentityErrorToIEnumerableString(IEnumerable<IdentityError> errors);

        string Encode(string token);

        string Decode(string code);
    }
}
