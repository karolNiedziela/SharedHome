using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Entities;
using SharedHome.Shared.Exceptions.Common;

namespace SharedHome.Application.Identity.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.PersonId.ToString());
            var result = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException(result);
            }

            return Unit.Value;
        }
    }
}
