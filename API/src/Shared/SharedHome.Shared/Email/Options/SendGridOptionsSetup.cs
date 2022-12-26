using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SharedHome.Shared.Email.Options
{
    internal class SendGridOptionsSetup : IConfigureOptions<SendGridOptions>
    {
        public const string SectionName = "SendGrid";

        private readonly IConfiguration _configuration;

        public SendGridOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SendGridOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
