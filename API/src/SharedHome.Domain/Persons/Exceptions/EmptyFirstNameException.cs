using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Persons.Exceptions
{
    public class EmptyFirstNameException : SharedHomeException
    {
        public EmptyFirstNameException() : base("First name cannot be empty.")
        {
        }
    }
}
