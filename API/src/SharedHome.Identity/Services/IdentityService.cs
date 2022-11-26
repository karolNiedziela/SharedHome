using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SharedHome.Identity.Entities;
using System.Text;

namespace SharedHome.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Cloudinary _cloudinary;

        public IdentityService(UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            _userManager = userManager;
            _cloudinary = cloudinary;
        }

        public async Task<string> GetEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var code = Encode(token);

            return code;
        }

        public IEnumerable<string> MapIdentityErrorToIEnumerableString(IEnumerable<IdentityError> errors)
            => errors.Select(error => error.Description);
       
        public async Task AddUserImage(string userId, UserImage image)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user.Images.Any())
            {
                var deleteParams = new DeletionParams(user.Images.First().PublicId)
                {
                    ResourceType = ResourceType.Image
                };

                var results = await _cloudinary.DestroyAsync(deleteParams);

                user.Images.Clear();                
            }

            user.Images.Add(image);

            await _userManager.UpdateAsync(user);
        }

        public async Task<string> GetProfileImage(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user.Images.Any() ? user.Images.First().Url : string.Empty;
        }

        public string Encode(string token)
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public string Decode(string code)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
    }
}
