using SharedHome.Domain.Persons.Exceptions;

namespace SharedHome.Domain.Persons.ValueObjects
{
    public record LastName
    {
        public string Value { get; }

        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyLastNameException();
            }
            Value = value;
        }

        public static implicit operator string(LastName lastName) => lastName.Value;

        public static implicit operator LastName(string value) => new(value);
    }
}
