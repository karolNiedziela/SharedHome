using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Application.Identity.Exceptions
{
    public class InvalidImageFormatException : SharedHomeException
    {
        public override string ErrorCode => "InvalidImageFormat";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidImageFormatException() : base("Invalid format of image.")
        {
        }
    }
}
