using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using SharedHome.Shared.Abstractions.Responses;
using System.Net;
using System.Reflection;

namespace SharedHome.Shared.Exceptions
{
    public class ExceptionToErrorResponseMapper : IExceptionToErrorResponseMapper
    {
        private readonly IStringLocalizer _localizer;

        public ExceptionToErrorResponseMapper(IStringLocalizerFactory localizerFactory)
        {
            _localizer = localizerFactory.Create("SharedHomeExceptionMessage", "SharedHome.Api");
        }

        public ErrorResponse Map(Exception exception)
            => exception switch
            {
                SharedHomeException ex => new ErrorResponse(HttpStatusCode.BadRequest, GetFormattedErrors(ex)),

                IdentityException ex => new ErrorResponse(HttpStatusCode.BadRequest, ex.Errors),

                ValidatorException ex => new ErrorResponse(HttpStatusCode.BadRequest, ex.ErrorMessages),

                _ => new ErrorResponse(HttpStatusCode.InternalServerError, "There was an error.")
            };

        private string GetFormattedErrors(SharedHomeException exception)
        {
            var resourceStringValue = _localizer.GetString(exception.ErrorCode);

            if (resourceStringValue.ResourceNotFound)
            {
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
    }
}
