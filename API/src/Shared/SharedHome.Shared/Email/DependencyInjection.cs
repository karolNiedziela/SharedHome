using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;
using SharedHome.Shared.Options;

namespace SharedHome.Shared.Email
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SendGridSettings>(configuration.GetSection(SendGridSettings.SectionName));

            var sendGridSettings = configuration.GetOptions<SendGridSettings>(SendGridSettings.SectionName);
            services.AddSendGrid(options =>
            {
                options.ApiKey = sendGridSettings.ApiKey;
            });

            services.AddTransient<IIdentityEmailSender, ConfirmationEmailSender>();

            return services;
        }
    }
}
