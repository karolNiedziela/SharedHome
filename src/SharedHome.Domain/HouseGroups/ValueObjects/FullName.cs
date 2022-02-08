using SharedHome.Domain.HouseGroups.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.ValueObjects
{
    public record FullName
    {
        public string Value { get; }

        public FullName(string value)
        {
            var splitted = value.Split(" ");
            if (splitted.Length != 2 || 
                string.IsNullOrWhiteSpace(splitted[0]) || 
                string.IsNullOrWhiteSpace(splitted[1]))
            {
                throw new FullNameFormatException();
            }

            Value = value;
        }

        public static implicit operator FullName(string value) => new(value);

        public static implicit operator string(FullName fullName) => fullName.Value;
    }
}
