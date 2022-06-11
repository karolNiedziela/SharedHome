using MailKit.Net.Smtp;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;

namespace SharedHome.Shared.Email
{
    public abstract class BaseEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger _logger;
        protected readonly IStringLocalizer _localizer;

        public BaseEmailSender(IOptions<EmailSettings> emailOptions, ILogger logger, IStringLocalizerFactory localizerFactory)
        {
            _emailSettings = emailOptions.Value;
            _logger = logger;
            _localizer = localizerFactory.Create("EmailTemplates", "SharedHome.Api");
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
                _logger.LogInformation("Email sent to '{emailAddress}", string.Join(", ", emailMessage.Recipients));
            }
            catch (Exception ex)
            {
                // TODO: Throw proper exception
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
            mimeMessage.From.Add(MailboxAddress.Parse(_emailSettings.Address));
            mimeMessage.To.AddRange(emailMessage.Recipients);
            mimeMessage.Subject = emailMessage.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = emailMessage.Body
            };

            if (emailMessage.Attachments is not null)
            {
                byte[] fileBytes;
                foreach (var attachment in emailMessage.Attachments)
                {
                    if (attachment.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        builder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                    }
                }
            }

            mimeMessage.Body = builder.ToMessageBody();

            return mimeMessage;
        }
    }
}
