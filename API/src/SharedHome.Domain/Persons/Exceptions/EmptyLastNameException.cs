using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Persons.Exceptions
{
    public class EmptyLastNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyLastName";

        public EmptyLastNameException() : base("Last name cannot be empty.")
        {
        }
    }
}
