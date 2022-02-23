using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedHome.Shared.Options
{
    public static class Extensions
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectioName) where TOptions : new()
        {
            var options = new TOptions();
            configuration.GetSection(sectioName).Bind(options);

            return options;
        }
    }
}
