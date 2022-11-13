using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF.Initializers
{
    public class InitializerOptionsSetup : IConfigureOptions<InitializerOptions>
    {
        public const string SectionName = "Initializer";

        private readonly IConfiguration _configuration;

        public InitializerOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(InitializerOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
