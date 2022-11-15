using MediatR;
using Microsoft.Extensions.Logging;
using SharedHome.Application.Identity.Commands.ConfirmEmail;
using SharedHome.Application.Identity.Commands.Register;
using SharedHome.Application.Identity.Queries.Login;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace SharedHome.Application.PipelineBehaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        private static readonly List<string> _requestsNotForSerialization = new() { typeof(ConfirmEmailCommand).Name, typeof(RegisterCommand).Name, typeof(LoginQuery).Name };

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
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
            _logger.LogInformation("Begin Request Id: {UniqueId}, " +
                "Request name: {RequestName}, " +
                "Request json: {RequestJson}",
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

            _logger.LogInformation("End Request Id: {UniqueId}, " +
                "Request name: {RequestName}, " +
                "Total request time {ElapsedMs}[ms], " +
                "Response json: {ResponseJson}",
                uniqueId,
                requestName,
                timer.ElapsedMilliseconds,
                responseJson);

            return response;
        }
    }
}
