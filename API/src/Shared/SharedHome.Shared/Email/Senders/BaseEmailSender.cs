using MailKit.Net.Smtp;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using SharedHome.Shared.Constants;
using SharedHome.Shared.Email.Options;
using SharedHome.Shared.Extensions;
using System.Reflection;

namespace SharedHome.Shared.Email.Senders
{
    public abstract class BaseEmailSender
    {
        private readonly EmailOptions _emailSettings;
        private readonly GeneralOptions _generalOptions;
        private readonly SendGridOptions _sendGridOptions;
        protected readonly ILogger _logger;
        protected readonly IStringLocalizer _localizer;

        public BaseEmailSender(
            IOptions<EmailOptions> emailOptions,
            ILogger logger,
            IStringLocalizerFactory localizerFactory,
            IOptions<GeneralOptions> generalOptions,
            IOptions<SendGridOptions> sendGridOptions)
        {
            _emailSettings = emailOptions.Value;
            _logger = logger;
            _localizer = localizerFactory.Create(Resources.EmailTemplates, Assembly.GetEntryAssembly()!.GetName().Name!);
            _generalOptions = generalOptions.Value;
            _sendGridOptions = sendGridOptions.Value;
        }

        public async Task SendAsync(EmailMessage emailMessage)
        {
            if (EnvironmentExtensions.IsProduction)
            {
                await SendWithSendGrid(emailMessage);
                return;
            }

           await SendWithGmailSmtp(emailMessage);
        }

        private async Task SendWithSendGrid(EmailMessage emailMessage)
        {
            var client = new SendGridClient(_sendGridOptions.ApiKey);
            var from = string.IsNullOrEmpty(emailMessage.From) ?
                new EmailAddress(_sendGridOptions.DefaultFrom, _sendGridOptions.DefaultFromName) :
                new EmailAddress(_sendGridOptions.DefaultFrom, emailMessage.From);

            foreach (var recipient in emailMessage.Recipients)
            {
                var to = new EmailAddress(recipient.Address, recipient.Name);
                var singleEmail = MailHelper.CreateSingleEmail(from, to, emailMessage.Subject, string.Empty, emailMessage.Body);
                var response = await client.SendEmailAsync(singleEmail);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Email with {subject} sent to {emailAddress}", emailMessage.Subject, string.Join(", ", recipient.Address));
                }
                else
                {
                    _logger.LogWarning("Failed Email with {subject} sent to {emailAddress}", emailMessage.Subject, string.Join(", ", recipient.Address));
                }
            }

        }

        private async Task SendWithGmailSmtp(EmailMessage emailMessage)
        {
            var mimeMessage = ConvertToMimeMessage(emailMessage);

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                await smtp.AuthenticateAsync(_emailSettings.Address, _emailSettings.Password);
                await smtp.SendAsync(mimeMessage);
                _logger.LogInformation("Email with {subject} sent to {emailAddress}", emailMessage.Subject, string.Join(", ", emailMessage.Recipients));
            }
            catch (Exception ex)
            {
                // TODO: Throw proper exception
                _logger.LogWarning("Email not sent to {emailAddress}.", string.Join(", ", emailMessage.Recipients));
                throw new Exception(ex.Message);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
                smtp.Dispose();
            }
        }

        protected string BuildClientRedirect(string redirectUrl) 
            => _generalOptions.AngularClientUri + redirectUrl;

        private MimeMessage ConvertToMimeMessage(EmailMessage emailMessage)
        {
            var mimeMessage = new MimeMessage();
            var from = string.IsNullOrEmpty(emailMessage.From) ?
               MailboxAddress.Parse(_emailSettings.Address) :
               MailboxAddress.Parse(emailMessage.From);
            mimeMessage.From.Add(from);
            mimeMessage.To.AddRange(emailMessage.Recipients);
            mimeMessage.Subject = emailMessage.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = emailMessage.Body
            };
         

            mimeMessage.Body = builder.ToMessageBody();

            return mimeMessage;
        }
    }
}
