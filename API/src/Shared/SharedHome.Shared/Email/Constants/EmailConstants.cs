namespace SharedHome.Shared.Email.Constants
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

        public class ForgotPasswordConstants
        {
            public const string Template = "forgotpassword.html";

            public const string Link = "{link}";

            public const string LinkReplacement = "https://localhost:7073/api/identity/forgotpassword?email={0}&code={1}";

            public const string Subject = "Forgot Password";
        }
    }
}
