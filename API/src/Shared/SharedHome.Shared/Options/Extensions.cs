using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedHome.Shared.Options
{
    public static class Extensions
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectioName) where TOptions : class, new()
        {
            var settings = new TOptions();
            configuration.GetSection(sectioName).Bind(settings);

            return settings;
        }
    }
}
