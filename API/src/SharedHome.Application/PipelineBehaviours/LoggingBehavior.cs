using MediatR;
using Microsoft.Extensions.Logging;
using SharedHome.Application.Authentication.Commands.ConfirmEmail;
using SharedHome.Application.Authentication.Commands.Register;
using SharedHome.Application.Authentication.Queries.Login;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace SharedHome.Application.PipelineBehaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        private static List<Type> _requestsToSkip = new() { typeof(ConfirmEmailCommand), typeof(RegisterCommand), typeof(LoginQuery)}; 

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            if (_requestsToSkip.Any(x => x is TRequest))
            {
                return await next();
            }

            var uniqueId = Guid.NewGuid().ToString();
            var requestJson = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            _logger.LogInformation("Begin Request Id: {uniqueId}, " +
                "Request name: {RequestName}, " +
                "Request json: {requestJson}", 
                uniqueId, 
                requestName, 
                requestJson);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();
            var responseJson = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            timer.Stop();

            _logger.LogInformation("End Request Id: {uniqueId}, " +
                "Request name: {RequestName}, " +
                "Rotal request time {elapsedMs}[ms], " +
                "Response json: {responseJson}", 
                uniqueId, 
                requestName,
                timer.ElapsedMilliseconds,
                responseJson);

            return response;
        }

    }
}
