using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SharedHome.Shared.Options;

namespace SharedHome.Infrastructure.EF.Options
{
    public class MySQLOptionsSetup : IConfigureOptions<MySQLOptions>
    {
        public const string SectionName = "MySQL";

        private readonly IConfiguration _configuration;

        public MySQLOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(MySQLOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
