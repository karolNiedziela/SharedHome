using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;
using SharedHome.Shared.Email.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Email
{
    public class ConfirmationEmailSender : BaseEmailSender, IIdentityEmailSender
    {
        private readonly Dictionary<string, string> Replacements = new();

        public ConfirmationEmailSender(EmailOptions emailOptions) : base(emailOptions)
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
