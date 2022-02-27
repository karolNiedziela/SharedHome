using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;
using System.Text.Json;

namespace SharedHome.Shared.Exceptions
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionToErrorResponseMapper _exceptionMapper;
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(IExceptionToErrorResponseMapper exceptionMapper, ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _exceptionMapper = exceptionMapper;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            _logger.LogWarning($"{exception.Source} : {exception.Message}");
            var errorResponse = _exceptionMapper.Map(exception);

            context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);

            var isResponseEmpty = errorResponse?.Errors.Count == 0;

            if (isResponseEmpty)
            {
                await context.Response.WriteAsync(string.Empty);
            }

            context.Response.ContentType = "application/json";

            var payload = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(payload);
        }
    }
}
