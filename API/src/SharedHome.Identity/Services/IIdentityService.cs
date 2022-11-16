using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Entities;

namespace SharedHome.Infrastructure.Identity.Services
{
    public interface IIdentityService
    {
        Task<string> GetEmailConfirmationTokenAsync(ApplicationUser user);

        IEnumerable<string> MapIdentityErrorToIEnumerableString(IEnumerable<IdentityError> errors);

        Task AddUserImage(string userId, UserImage image);

        string Encode(string token);

        string Decode(string code);

    }
}
