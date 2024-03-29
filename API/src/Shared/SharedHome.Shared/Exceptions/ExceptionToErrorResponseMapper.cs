﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SharedHome.Shared.Attributes;
using SharedHome.Shared.Constants;
using SharedHome.Shared.Time;
using System.Net;
using System.Reflection;

namespace SharedHome.Shared.Exceptions.Common
{
    internal class ExceptionToErrorResponseMapper : IExceptionToErrorResponseMapper
    {
        private readonly IStringLocalizer _localizer;
        private readonly ILogger<ExceptionToErrorResponseMapper> _logger;
        private readonly ITimeProvider _timeProvider;

        public ExceptionToErrorResponseMapper(
            IStringLocalizerFactory localizerFactory,
            ILogger<ExceptionToErrorResponseMapper> logger,
            ITimeProvider timeProvider)
        {
            _localizer = localizerFactory.Create(Resources.SharedHomeExceptionMessage, Assembly.GetEntryAssembly()!.GetName().Name!);
            _logger = logger;
            _timeProvider = timeProvider;
        }

        public ErrorResponse Map(Exception exception)
            => exception switch
            {
                SharedHomeException ex => new ErrorResponse(ex.StatusCode, GetFormattedErrors(ex)),

                IdentityException ex => new ErrorResponse(HttpStatusCode.BadRequest, ex.Errors),

                _ => HandleUnexpectedError(exception)
            };

        private string GetFormattedErrors(SharedHomeException exception)
        {
            var resourceStringValue = _localizer.GetString(exception.ErrorCode);

            if (resourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning("Resource {exceptionCode} not found.", 
                    exception.ErrorCode);
                return exception.Message;
            }

            var properties = exception.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(OrderAttribute)))
                .OrderBy(prop => prop.GetCustomAttribute<OrderAttribute>()?.Order);  
            
            if (!properties.Any())
            {
                return resourceStringValue.Value;
            }

            var values = properties.Select(prop => prop.GetValue(exception, null)).ToArray();

            var formattedMessage = string.Format(resourceStringValue.Value, values);            

            return formattedMessage;
        }

        private ErrorResponse HandleUnexpectedError(Exception exception)
        {
            _logger.LogError("{DateTimeUtc}: {message}", _timeProvider.CurrentDate(), exception.Message);
            _logger.LogError("{DateTimeUtc}: {stackTrace}", _timeProvider.CurrentDate(), exception.StackTrace);

            return new ErrorResponse(HttpStatusCode.InternalServerError, "An unexpected error occurred.");
        }
    }
}
