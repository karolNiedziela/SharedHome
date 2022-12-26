using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharedHome.Shared.Email.Options;
using static SharedHome.Shared.Email.Constants.EmailConstants;

namespace SharedHome.Shared.Email.Senders
{
    public class ForgotPasswordEmailSender : BaseEmailSender, IIdentityEmailSender<ForgotPasswordEmailSender>
    {
        private readonly Dictionary<string, string> Replacements = new();

        public ForgotPasswordEmailSender(
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

            Replacements.Add(ForgotPasswordConstants.Link, string.Format(BuildClientRedirect(ForgotPasswordConstants.ClientUriSuffix), email, code));

            emailMessage
               .WithSubject(_localizer.GetString(ForgotPasswordConstants.Subject))
               .WithBody(_localizer.GetString(ForgotPasswordConstants.Template), Replacements)
               .WithRecipients(new string[] { email });

            await SendAsync(emailMessage);
        }
    }
}
