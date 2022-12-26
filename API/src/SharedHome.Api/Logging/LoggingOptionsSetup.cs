using Microsoft.Extensions.Options;

namespace SharedHome.Api.Logging
{
    public class LoggingOptionsSetup : IConfigureOptions<LoggingOptions>
    {
        public const string SectionName = "Logging";

        private readonly IConfiguration _configuration;

        public LoggingOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(LoggingOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
