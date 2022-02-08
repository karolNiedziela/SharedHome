using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Responses
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public HashSet<string> Errors { get; set; } = new HashSet<string>();

        public ErrorResponse(HttpStatusCode statusCode, HashSet<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
