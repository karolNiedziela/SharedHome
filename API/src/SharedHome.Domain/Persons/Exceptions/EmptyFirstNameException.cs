using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.Persons.Exceptions
{
    public class EmptyFirstNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyFirstName";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyFirstNameException() : base("First name cannot be empty.")
        {
        }
    }
}
