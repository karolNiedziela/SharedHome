using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SharedHome.Domain.Persons.ValueObjects;
using SharedHome.Identity.Entities;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Exceptions.Common;
using System.Text;

namespace SharedHome.Application.Identity.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _identityService;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            IApplicationUserService identityService,
            ILogger<ResetPasswordCommandHandler> logger)
        {
            _userManager = userManager;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                _logger.LogInformation("User with email: '{email}' was not found.", request.Email);
                return Unit.Value;
            }

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException(result);
            }

            return Unit.Value;
        }
    }
}
