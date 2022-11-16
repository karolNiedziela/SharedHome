using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SharedHome.Identity.Authentication
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        public const string SectionName = "Jwt";

        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
