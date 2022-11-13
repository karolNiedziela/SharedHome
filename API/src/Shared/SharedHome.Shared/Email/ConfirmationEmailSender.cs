using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;
using SharedHome.Shared.Email.Templates;

namespace SharedHome.Shared.Email
{
    public class ConfirmationEmailSender : BaseEmailSender, IIdentityEmailSender
    {
        private readonly GeneralOptions _generalOptions;

        private readonly Dictionary<string, string> Replacements = new();

        public ConfirmationEmailSender(IOptions<EmailOptions> emailOptions, ILogger<ConfirmationEmailSender> logger, IStringLocalizerFactory localizerFactory, IOptions<GeneralOptions> generalOptions) 
            : base(emailOptions, logger, localizerFactory)
        {
            _generalOptions = generalOptions.Value;
        }

        public async Task SendAsync(string email, string code)
        {
            var emailMessage = new EmailMessage();

            Replacements.Add(EmailConstants.ConfirmationEmailConstants.ConfirmationLink, string.Format(_generalOptions.ConfirmationEmailAngularClient, email, code));

            emailMessage
                .WithSubject(_localizer.GetString(EmailConstants.ConfirmationEmailConstants.Subject))
                .WithBody(_localizer.GetString(EmailConstants.ConfirmationEmailConstants.Template), Replacements)
                .WithRecipients(new string[] { email});

            await SendAsync(emailMessage);
        }
    }
}
