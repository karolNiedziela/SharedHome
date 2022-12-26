using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharedHome.Shared.Email.Constants;
using SharedHome.Shared.Email.Options;
using static SharedHome.Shared.Email.Constants.EmailConstants;

namespace SharedHome.Shared.Email.Senders
{
    public class ConfirmationEmailSender : BaseEmailSender, IIdentityEmailSender<ConfirmationEmailSender>
    {
        private readonly Dictionary<string, string> Replacements = new();

        public ConfirmationEmailSender(
            IOptions<EmailOptions> emailOptions,
            ILogger<ForgotPasswordEmailSender> logger,
            IStringLocalizerFactory localizerFactory,
            IOptions<GeneralOptions> generalOptions,
            IOptions<SendGridOptions> sendGridOptions)
            : base(emailOptions, logger, localizerFactory, generalOptions, sendGridOptions)
        {
        }

        public async Task SendAsync(string email, string code)
        {
            var emailMessage = new EmailMessage();

            Replacements.Add(ConfirmationEmailConstants.ConfirmationLink, string.Format(BuildClientRedirect(ConfirmationEmailConstants.ClientUriSuffix), email, code));

            emailMessage
                .WithSubject(_localizer.GetString(EmailConstants.ConfirmationEmailConstants.Subject))
                .WithBody(_localizer.GetString(EmailConstants.ConfirmationEmailConstants.Template), Replacements)
                .WithRecipients(new string[] { email});

            await SendAsync(emailMessage);
        }
    }
}
