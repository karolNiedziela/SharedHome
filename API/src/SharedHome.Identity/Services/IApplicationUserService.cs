using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Entities;

namespace SharedHome.Infrastructure.Identity.Services
{
    public interface IApplicationUserService
    {
        Task AddUserImage(string userId, UserImage image);

        Task<string> GetProfileImage(string userId);
    }
}
