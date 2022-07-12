using System.Net;

namespace SharedHome.Shared.Abstractions.Exceptions
{
    public class InvalidEnumException : SharedHomeException
    {
        public override string ErrorCode => "InvalidEnum";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidEnumException() : base("Invalid enum value.")
        {

        }

    }
}
