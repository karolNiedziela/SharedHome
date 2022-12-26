using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Email.Options;
using SharedHome.Shared.Email.Senders;

namespace SharedHome.Shared.Email
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmail(this IServiceCollection services)
        {
            services.ConfigureOptions<EmailOptionsSetup>();
            services.ConfigureOptions<SendGridOptionsSetup>();

            services.AddScoped<IIdentityEmailSender<ConfirmationEmailSender>, ConfirmationEmailSender>();
            services.AddScoped<IIdentityEmailSender<ForgotPasswordEmailSender>, ForgotPasswordEmailSender>();

            return services;
        }
    }
}
