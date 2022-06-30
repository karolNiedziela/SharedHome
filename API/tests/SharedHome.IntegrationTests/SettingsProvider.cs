using Microsoft.Extensions.Configuration;
using SharedHome.Shared.Settings;

namespace SharedHome.IntegrationTests
{
    public sealed class SettingsProvider
    {
        private readonly IConfigurationRoot _configuration;

        public SettingsProvider()
        {
            _configuration = GetConfigurationRoot();
        }

        public T Get<T>(string sectionName) where T : class, new() => _configuration.GetSettings<T>(sectionName);

        private static IConfigurationRoot GetConfigurationRoot()
            => new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", true)
            .AddEnvironmentVariables()
            .Build();
    }
}
