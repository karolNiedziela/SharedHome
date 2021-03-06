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
        private readonly GeneralSettings _generalSettings;

        private readonly Dictionary<string, string> Replacements = new();

        public ConfirmationEmailSender(IOptions<EmailSettings> emailOptions, ILogger<ConfirmationEmailSender> logger, IStringLocalizerFactory localizerFactory, IOptions<GeneralSettings> generalOptions) 
            : base(emailOptions, logger, localizerFactory)
        {
            _generalSettings = generalOptions.Value;
        }

        public async Task SendAsync(string email, string code)
        {
            var emailMessage = new EmailMessage();

            Replacements.Add(EmailConstants.ConfirmationEmailConstants.ConfirmationLink, string.Format(_generalSettings.ConfirmationEmailAngularClient, email, code));

            emailMessage
                .WithSubject(_localizer.GetString(EmailConstants.ConfirmationEmailConstants.Subject))
                .WithBody(_localizer.GetString(EmailConstants.ConfirmationEmailConstants.Template), Replacements)
                .WithRecipients(new string[] { email});

            await SendAsync(emailMessage);
        }
    }
}
