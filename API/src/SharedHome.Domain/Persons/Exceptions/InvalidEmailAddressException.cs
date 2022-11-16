using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.Persons.Exceptions
{
    public class InvalidEmailAddressException : SharedHomeException
    {
        public override string ErrorCode => "InvalidEmailAddress";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidEmailAddressException() : base("Invalid email address format.")
        {
        }
    }
}
