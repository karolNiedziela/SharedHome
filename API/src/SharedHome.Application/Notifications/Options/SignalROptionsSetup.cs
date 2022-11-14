using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SharedHome.Application.Notifications.Options
{
    public class SignalROptionsSetup : IConfigureOptions<SignalROptions>
    {
        public const string SectionName = "SignalR";

        private readonly IConfiguration _configuration;

        public SignalROptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SignalROptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
