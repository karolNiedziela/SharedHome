using SharedHome.Shared.Abstractions.Exceptions;
using SharedHome.Shared.Abstractions.Responses;
using System.Net;

namespace SharedHome.Shared.Exceptions
{
    public class ExceptionToErrorResponseMapper : IExceptionToErrorResponseMapper
    {
        public ErrorResponse Map(Exception exception)
            => exception switch
            {
                SharedHomeException ex => new ErrorResponse(HttpStatusCode.BadRequest, ex.Message),

                IdentityException ex => new ErrorResponse(HttpStatusCode.Unauthorized, ex.Errors),

                ValidatorException ex => new ErrorResponse(HttpStatusCode.BadRequest, ex.ErrorMessages),

                _ => new ErrorResponse(HttpStatusCode.InternalServerError, "There was an error.")
            };


    }
}
