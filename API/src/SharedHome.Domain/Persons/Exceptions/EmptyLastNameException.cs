using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Persons.Exceptions
{
    public class EmptyLastNameException : SharedHomeException
    {
        public EmptyLastNameException() : base("Last name cannot be empty.")
        {
        }
    }
}
