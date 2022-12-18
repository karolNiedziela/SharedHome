using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedHome.Application.Identity.Dto;
using SharedHome.Application.Identity.Queries.GetProfileImage;
using SharedHome.Identity.Entities;

namespace SharedHome.Infrastructure.EF.Queries.Identity.Handlers
{
    public class GetProfileImageHandler : IRequestHandler<GetProfileImage, ProfileImageDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetProfileImageHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ProfileImageDto> Handle(GetProfileImage request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.PersonId.ToString());

            var profileImageUrl = user.Images.FirstOrDefault()?.Url;

            return new ProfileImageDto
            {
                Url = profileImageUrl,
            };
        }
    }
}
