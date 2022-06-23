using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedHome.Shared.Settings
{
    public static class Extensions
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration, string sectioName) where TSettings : class, new()
        {
            var settings = new TSettings();
            configuration.GetSection(sectioName).Bind(settings);

            return settings;
        }
    }
}
