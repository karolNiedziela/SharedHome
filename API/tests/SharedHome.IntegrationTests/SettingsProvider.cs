using Microsoft.Extensions.Configuration;
using SharedHome.Shared.Options;

namespace SharedHome.IntegrationTests
{
    public sealed class SettingsProvider
    {
        private readonly IConfigurationRoot _configuration;

        public SettingsProvider()
        {
            _configuration = GetConfigurationRoot();
        }

        public T Get<T>(string sectionName) where T : class, new() => _configuration.GetOptions<T>(sectionName);

        private static IConfigurationRoot GetConfigurationRoot()
            => new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", true)
            .AddEnvironmentVariables()
            .Build();
    }
}
