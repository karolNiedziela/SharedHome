using SharedHome.Domain.HouseGroups.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            try
            {
                var emailAddress = new MailAddress(value);
                Value = emailAddress.Address;
            }
            catch
            {
                throw new EmailFormatException();
            }
        }

        public static implicit operator Email(string value) => new(value);

        public static implicit operator string(Email email) => email.Value;
    }
}
