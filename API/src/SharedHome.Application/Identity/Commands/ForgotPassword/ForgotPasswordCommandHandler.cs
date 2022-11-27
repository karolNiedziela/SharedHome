using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SharedHome.Identity.Entities;
using SharedHome.Identity.Exceptions;
using SharedHome.Shared.Email.Senders;
using System.Text;

namespace SharedHome.Application.Identity.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;
        private readonly IIdentityEmailSender<ForgotPasswordEmailSender> _emailSender;

        public ForgotPasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            ILogger<ForgotPasswordCommandHandler> logger,
            IIdentityEmailSender<ForgotPasswordEmailSender> emailSender)
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                _logger.LogInformation("User with email: '{email}' was not found.", request.Email);
                throw new InvalidCredentialsException();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            await _emailSender.SendAsync(user.Email, code);

            return Unit.Value;
        }
    }
}
