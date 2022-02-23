using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Email.Options;
using SharedHome.Shared.Options;

namespace SharedHome.Shared.Email
{
    public static class Extensions
    {
        public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            var emailOptions = configuration.GetOptions<EmailOptions>(EmailOptions.Email);
            services.AddSingleton(emailOptions);

            services.AddTransient<IIdentityEmailSender, ConfirmationEmailSender>();

            return services;
        }
    }
}
