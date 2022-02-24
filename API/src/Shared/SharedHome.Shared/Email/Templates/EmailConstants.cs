using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Email.Templates
{
    public static class EmailConstants
    {
        public class ConfirmationEmailConstants
        {
            public const string Template = "confirmationemail.html";

            public const string ConfirmationLink = "{confirmation_link}";

            public const string ConfirmationLinkReplacement = "https://localhost:7073/api/identity/confirmemail?email={0}&code={1}";

            public const string Subject = "Confirmation Email";
        }
    }
}
