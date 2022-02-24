using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Email
{
    public class EmailMessage
    {
        public List<MailboxAddress> Recipients { get; set; } = new();

        public string Subject { get; set; } = default!;

        public string Body { get; set; } = default!;

        // Key: Links, Value: LinkText

        public Dictionary<string, string> Links { get; set; } = new();

        public IFormFileCollection Attachments { get; set; } = default!;
    }
}
