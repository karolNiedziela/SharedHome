using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SharedHome.Domain.Persons.Events;
using SharedHome.Identity.Entities;
using SharedHome.Shared.Email.Senders;
using System.Text;

namespace SharedHome.Application.Persons.Events
{
    public class PersonCreatedEventHandler : INotificationHandler<PersonCreated>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityEmailSender<ConfirmationEmailSender> _emailSender;

        public PersonCreatedEventHandler(UserManager<ApplicationUser> userManager, IIdentityEmailSender<ConfirmationEmailSender> emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task Handle(PersonCreated notification, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(notification.Email);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            await _emailSender.SendAsync(notification.Email, code);
        }
    }
}
