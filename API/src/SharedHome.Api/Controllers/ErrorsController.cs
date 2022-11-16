using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Shared.Constants;
using SharedHome.Shared.Exceptions;

namespace SharedHome.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly IExceptionToErrorResponseMapper _exceptionMapper;
        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(IExceptionToErrorResponseMapper exceptionMapper, ILogger<ErrorsController> logger)
        {
            _exceptionMapper = exceptionMapper;
            _logger = logger;
        }

        [Route(ApiRoutes.Errors.Error)]
        public IActionResult HandleError()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            _logger.LogWarning("{exceptionSource} : {exceptionMessage}", exception!.Source, exception!.Message);

            var errorResponse = _exceptionMapper.Map(exception!);

            HttpContext.Items[HttpContextItemKeys.Errors] = errorResponse.Errors;

            return Problem(statusCode: (int)errorResponse.StatusCode);
        } 

        [Route(ApiRoutes.Errors.ErrorDevelopment)]
        public IActionResult HandleErrorDevelopment([FromServices]IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            _logger.LogWarning("{exceptionSource} : {exceptionMessage}", exception!.Source, exception!.Message);

            var errorResponse = _exceptionMapper.Map(exception!);

            HttpContext.Items[HttpContextItemKeys.Errors] = errorResponse.Errors;

            return Problem(statusCode: (int)errorResponse.StatusCode, detail: exception!.StackTrace);
        }
    }
}
