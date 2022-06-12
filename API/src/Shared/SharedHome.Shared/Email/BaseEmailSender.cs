using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;

namespace SharedHome.Shared.Email
{
    public abstract class BaseEmailSender
    {
        private readonly SendGridSettings _sendGridSettings;
        private readonly ILogger _logger;

        protected readonly IStringLocalizer _localizer;


        public BaseEmailSender(IOptions<SendGridSettings> sendGridOptions, ILogger logger, IStringLocalizerFactory localizerFactory)
        {
            _sendGridSettings = sendGridOptions.Value;
            _logger = logger;
            _localizer = localizerFactory.Create("EmailTemplates", "SharedHome.Api");
        }

        public async Task SendAsync(EmailMessage emailMessage)
        {
            var from = string.IsNullOrEmpty(emailMessage.From) ?
               new EmailAddress(_sendGridSettings.DefaultFromEmailAddress) :
               new EmailAddress(emailMessage.From);

            var sendGridClient = new SendGridClient(_sendGridSettings.ApiKey);
            var message = new SendGridMessage
            {
                From = from,
                Subject = emailMessage.Subject,
                HtmlContent = emailMessage.Body,
                ReplyTos = emailMessage.ReplyTos
            };

            var response = await sendGridClient.SendEmailAsync(message).ConfigureAwait(false);
            
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Email with {subject} sent to {emailAddress}", emailMessage.Subject, string.Join(", ", emailMessage.ReplyTos));
            }
            else
            {
                _logger.LogWarning("Email not sent to {emailAddress} with status code: {statusCode}", string.Join(", ", emailMessage.ReplyTos), response.StatusCode);
            }
        }
     
    }
}
