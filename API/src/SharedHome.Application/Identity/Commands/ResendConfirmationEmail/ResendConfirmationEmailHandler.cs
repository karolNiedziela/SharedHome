using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SharedHome.Identity.Entities;
using SharedHome.Identity.Exceptions;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Email.Senders;
using System.Text;

namespace SharedHome.Application.Identity.Commands.ResendConfirmationEmail
{
    public class ResendConfirmationEmailHandler : IRequestHandler<ResendConfirmationEmailCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityEmailSender<ConfirmationEmailSender> _emailSender;

        public ResendConfirmationEmailHandler(UserManager<ApplicationUser> userManager, IIdentityEmailSender<ConfirmationEmailSender> emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<ApiResponse<string>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByEmailAsync(request.Email);
            if (applicationUser is null)
            {
                throw new InvalidCredentialsException();
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            await _emailSender.SendAsync(applicationUser.Email, code);

            return new ApiResponse<string>("Check your mailbox and confirm email to get fully access.");
        }
    }
}
