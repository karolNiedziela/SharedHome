using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Application.Persons.Exceptions
{
    public class PersonWithEmailNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "PersonWithEmailNotFound";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        [Order]
        public string Email { get; }

        public PersonWithEmailNotFoundException(string email) : base($"Person with email {email} not found.")
        {
            Email = email;
        }
    }
}
