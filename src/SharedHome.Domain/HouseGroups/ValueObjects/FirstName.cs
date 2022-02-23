using SharedHome.Domain.HouseGroups.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.ValueObjects
{
    public class FirstName
    {
        public string Value { get; }

        public FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFirstNameException();
            }

            Value = value;
        }

        public static implicit operator string(FirstName lastName) => lastName.Value;

        public static implicit operator FirstName(string value) => new(value);
    }
}
