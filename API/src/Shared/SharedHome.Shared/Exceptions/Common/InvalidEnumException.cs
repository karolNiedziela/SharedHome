using System.Net;

namespace SharedHome.Shared.Exceptions.Common
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
