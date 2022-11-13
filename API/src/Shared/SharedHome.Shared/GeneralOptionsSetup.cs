using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SharedHome.Shared
{
    public class GeneralOptionsSetup : IConfigureOptions<GeneralOptions>
    {
        public const string SectionName = "General";

        private readonly IConfiguration _configuration;

        public GeneralOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(GeneralOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
