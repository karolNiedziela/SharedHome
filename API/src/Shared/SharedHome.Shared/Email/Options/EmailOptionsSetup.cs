using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SharedHome.Shared.Email.Options
{
    public class EmailOptionsSetup : IConfigureOptions<EmailOptions>
    {
        public const string SectionName = "Email";

        private readonly IConfiguration _configuration;

        public EmailOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(EmailOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
