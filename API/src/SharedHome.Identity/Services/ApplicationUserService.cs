using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Entities;

namespace SharedHome.Infrastructure.Identity.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Cloudinary _cloudinary;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            _userManager = userManager;
            _cloudinary = cloudinary;
        }
       
        public async Task AddUserImageAsync(string userId, UserImage image)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user.Images.Any())
            {
                var deleteParams = new DeletionParams(user.Images.First().PublicId)
                {
                    ResourceType = ResourceType.Image
                };

                var result = await _cloudinary.DestroyAsync(deleteParams);

                user.Images.Clear();                
            }

            user.Images.Add(image);

            await _userManager.UpdateAsync(user);
        }

        public async Task<string> GetProfileImageAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user.Images.Any() ? user.Images.First().Url : string.Empty;
        }         
    }
}
