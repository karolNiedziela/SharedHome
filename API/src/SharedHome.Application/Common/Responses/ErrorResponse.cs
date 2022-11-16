using System.Net;

namespace SharedHome.Shared.Application.Responses
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public List<string> Errors { get; set; } = new ();

        public ErrorResponse(HttpStatusCode statusCode, IEnumerable<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors.ToList();
        }

        public ErrorResponse(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            Errors.Add(errorMessage);
        }
    }
}
