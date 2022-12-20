using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedHome.Application.Identity.Dto;
using SharedHome.Application.Identity.Queries.GetUserInformation;
using SharedHome.Identity.Entities;

namespace SharedHome.Infrastructure.EF.Queries.Identity.Handlers
{
    public class GetUserInformationHandler : IRequestHandler<GetUserInformationQuery, UserInformationDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserInformationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserInformationDto> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.PersonId.ToString());
            var profileImageUrl = user.Images.FirstOrDefault()?.Url;

            return new UserInformationDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileImageUrl = profileImageUrl,
            };
        }
    }
}
