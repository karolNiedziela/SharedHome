using MailKit.Net.Smtp;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SharedHome.Shared.Constants;
using SharedHome.Shared.Email.Options;
using System.Reflection;

namespace SharedHome.Shared.Email.Senders
{
    public abstract class BaseEmailSender
    {
        private readonly EmailOptions _emailSettings;
        protected readonly ILogger _logger;
        protected readonly IStringLocalizer _localizer;

        public BaseEmailSender(IOptions<EmailOptions> emailOptions, ILogger logger, IStringLocalizerFactory localizerFactory)
        {
            _emailSettings = emailOptions.Value;
            _logger = logger;
            _localizer = localizerFactory.Create(Resources.EmailTemplates, Assembly.GetEntryAssembly()!.GetName().Name!);
        }

        public async Task SendAsync(EmailMessage emailMessage)
        {
            var mimeMessage = ConvertToMimeMessage(emailMessage);

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                await smtp.AuthenticateAsync(_emailSettings.Address, _emailSettings.Password);
                await smtp.SendAsync(mimeMessage);
                _logger.LogInformation("Email with {subject} sent to {emailAddress}", emailMessage.Subject, string.Join(", ", emailMessage.ReplyTos));
            }
            catch (Exception ex)
            {
                // TODO: Throw proper exception
                _logger.LogWarning("Email not sent to {emailAddress}.", string.Join(", ", emailMessage.ReplyTos));
                throw new Exception(ex.Message);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
                smtp.Dispose();
            }
        }

        private MimeMessage ConvertToMimeMessage(EmailMessage emailMessage)
        {
            var mimeMessage = new MimeMessage();
            var from = string.IsNullOrEmpty(emailMessage.From) ?
               MailboxAddress.Parse(_emailSettings.Address) :
               MailboxAddress.Parse(emailMessage.From);
            mimeMessage.From.Add(from);
            mimeMessage.To.AddRange(emailMessage.ReplyTos);
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
