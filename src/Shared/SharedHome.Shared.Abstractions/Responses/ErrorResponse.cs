using System.Net;

namespace SharedHome.Shared.Abstractions.Responses
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public ErrorResponse(HttpStatusCode statusCode, IEnumerable<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
