using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;

namespace SharedHome.Shared.Email
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

            services.AddTransient<IIdentityEmailSender, ConfirmationEmailSender>();

            return services;
        }
    }
}
