using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using SharedHome.Application.Notifications;
using SharedHome.Shared.Abstractions.Authentication;

namespace SharedHome.Api.HealthChecks.Custom
{
    public class SignalRHealthCheck : IHealthCheck
    {
        private readonly IServiceProvider _serviceProvider;

        public SignalRHealthCheck(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            HubConnection? connection = null;

            var authManager = _serviceProvider.GetService<IAuthManager>();
            var signalRSettings = _serviceProvider.GetService<IOptions<SignalRSettings>>();

            try
            {
                connection = new HubConnectionBuilder()
                    .WithUrl(signalRSettings!.Value.NotificationsUri, options =>
                    {
                        options.AccessTokenProvider = () =>
                        {
                            var authResponse = authManager!.Authenticate("", "", "", "", Array.Empty<string>());
                            return Task.FromResult(authResponse.AccessToken)!;
                        };
                    })
                    .Build();

                await connection.StartAsync(cancellationToken);

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
            finally
            {
                if (connection is not null)
                {
                    await connection.DisposeAsync();
                }
            }
        }
    }
}
