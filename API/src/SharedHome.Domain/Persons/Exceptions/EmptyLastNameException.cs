using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.Persons.Exceptions
{
    public class EmptyLastNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyLastName";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyLastNameException() : base("Last name cannot be empty.")
        {
        }
    }
}
