using SharedHome.Domain.Persons.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Persons.ValueObjects
{
    public record Email
    {
        public string Value { get; } = default!;

        private Email()
        {

        }

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
