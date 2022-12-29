using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Entities;

namespace SharedHome.Infrastructure.Identity.Services
{
    public interface IApplicationUserService
    {
        Task AddUserImageAsync(string userId, UserImage image);

        Task<string> GetProfileImageAsync(string userId);
    }
}
