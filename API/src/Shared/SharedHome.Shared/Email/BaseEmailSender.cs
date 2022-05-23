using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Email
{
    public abstract class BaseEmailSender
    {
        private readonly EmailOptions _emailOptions;
        private readonly ILogger _logger;

        public BaseEmailSender(EmailOptions emailOptions, ILogger logger)
        {
            _emailOptions = emailOptions;
            _logger = logger;
        }

        public async Task SendAsync(EmailMessage emailMessage)
        {
            var mimeMessage = ConvertToMimeMessage(emailMessage);

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_emailOptions.Host, _emailOptions.Port, true);
                await smtp.AuthenticateAsync(_emailOptions.Address, _emailOptions.Password);
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
            mimeMessage.From.Add(MailboxAddress.Parse(_emailOptions.Address));
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
