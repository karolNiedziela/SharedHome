using SharedHome.Domain.HouseGroups.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.ValueObjects
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
