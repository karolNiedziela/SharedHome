using Microsoft.Extensions.Logging;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;
using SharedHome.Shared.Email.Templates;

namespace SharedHome.Shared.Email
{
    public class ConfirmationEmailSender : BaseEmailSender, IIdentityEmailSender
    {
        private readonly Dictionary<string, string> Replacements = new();

        public ConfirmationEmailSender(EmailOptions emailOptions, ILogger<ConfirmationEmailSender> logger) 
            : base(emailOptions, logger)
        {
        }

        public async Task SendAsync(string email, string code)
        {
            var emailMessage = new EmailMessage();

            Replacements.Add(EmailConstants.ConfirmationEmailConstants.ConfirmationLink, string.Format(EmailConstants.ConfirmationEmailConstants.ConfirmationLinkReplacement, email, code));

            emailMessage
                .WithSubject(EmailConstants.ConfirmationEmailConstants.Subject)
                .WithBody(EmailConstants.ConfirmationEmailConstants.Template, Replacements)
                .WithRecipients(new string[] { email});

            await SendAsync(emailMessage);
        }
    }
}
