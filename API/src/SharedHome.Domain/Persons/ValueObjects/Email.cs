using SharedHome.Domain.Persons.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SharedHome.Domain.Persons.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string email)
        {
            if (!new EmailAddressAttribute().IsValid(email))
            {
                throw new InvalidEmailAddressException();
            }

            Value = email;
        }

        public static implicit operator string(Email email) => email.Value;
        
        public static implicit operator Email(string value) => new(value);
    }
}
