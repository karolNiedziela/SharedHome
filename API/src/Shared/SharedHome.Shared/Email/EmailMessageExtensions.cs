using MimeKit;

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

        public static EmailMessage WithRecipients(this EmailMessage emailMessage, IEnumerable<string> replyTos)
        {
            emailMessage.Recipients.AddRange(replyTos.Select(replyTo => MailboxAddress.Parse(replyTo)));

            return emailMessage;
        }

        private static string GetTemplatesPath(string emailTemplate)
        {
            var dir = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.FullName;
            return Path.Combine(dir, string.Format(@"src\Shared\SharedHome.Shared\Email\Templates\{0}", emailTemplate));
        }
    }
}
