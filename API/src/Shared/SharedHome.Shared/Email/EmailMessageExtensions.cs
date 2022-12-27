using MimeKit;
using SharedHome.Shared.Extensions;

namespace SharedHome.Shared.Email
{
    public static class EmailMessageExtensions
    {
        public static EmailMessage WithSubject(this EmailMessage emailMessage, string subject)
        {
            emailMessage.Subject = subject;

            return emailMessage;
        }

        public static EmailMessage WithBody(this EmailMessage emailMessage, string emailTemplate, Dictionary<string, string> replacements)
        {
            var file = File.ReadAllText(GetTemplatesPath(emailTemplate));

            foreach (var replacement in replacements)
            {
                file = file.Replace(replacement.Key, replacement.Value);
            }

            emailMessage.Body = file;

            return emailMessage;
        }

        public static EmailMessage WithRecipients(this EmailMessage emailMessage, IEnumerable<string> recipients)
        {
            emailMessage.Recipients.AddRange(recipients.Select(recipient => MailboxAddress.Parse(recipient)));

            return emailMessage;
        }

        private static string GetTemplatesPath(string emailTemplate)
        {
            var dir = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

            return EnvironmentExtensions.IsProduction ?
            Path.Combine(dir, string.Format(@"wwwroot\Email\Templates\{0}", emailTemplate)) :
            Path.Combine(dir, string.Format(@"Shared\SharedHome.Shared\Email\Templates\{0}", emailTemplate));
        }
    }
}
