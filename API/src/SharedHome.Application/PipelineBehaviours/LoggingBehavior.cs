using MediatR;
using Microsoft.Extensions.Logging;
using SharedHome.Application.Authentication.Commands.ConfirmEmail;
using SharedHome.Application.Authentication.Queries.Login;
using SharedHome.Application.Identity.Commands.ForgotPassword;
using SharedHome.Application.Identity.Commands.Register;
using SharedHome.Application.Identity.Commands.ResetPassword;
using SharedHome.Shared.Time;
using System.Diagnostics;
using System.Text.Json;

namespace SharedHome.Application.PipelineBehaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ITimeProvider _timeProvider;

        private static readonly List<string> _requestsNotForSerialization = new() 
        { 
            typeof(ConfirmEmailCommand).Name, 
            typeof(RegisterCommand).Name, 
            typeof(LoginQuery).Name,
            typeof(ConfirmEmailCommand).Name,
            typeof(ForgotPasswordCommand).Name,
            typeof(ResetPasswordCommand).Name
        };

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ITimeProvider timeProvider)
        {
            _logger = logger;
            _timeProvider = timeProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            var isRequestNotForSerialization = _requestsNotForSerialization.Any(x => x == requestName);

            var uniqueId = Guid.NewGuid().ToString();

            var requestJson = isRequestNotForSerialization ?
                 string.Empty :
                 JsonSerializer.Serialize(request, new JsonSerializerOptions
                 {
                     WriteIndented = true
                 });
            _logger.LogInformation("{DateTimeUtc}: Begin Request Id: {@UniqueId}, " +
                "Request name: {RequestName}, " +
                "Request json: {RequestJson}",
                _timeProvider.CurrentDate(),
                uniqueId,
                requestName,
                requestJson);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            var responseJson = isRequestNotForSerialization ?
                string.Empty :
                JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            timer.Stop();

            _logger.LogInformation("{DateTimeUtc}: End Request Id: {UniqueId}, " +
                "Request name: {RequestName}, " +
                "Total request time {ElapsedMs}[ms], " +
                "Response json: {ResponseJson}",
                _timeProvider.CurrentDate(),
                uniqueId,
                requestName,
                timer.ElapsedMilliseconds,
                responseJson);

            return response;
        }
    }
}
