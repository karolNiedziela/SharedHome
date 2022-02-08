using SharedHome.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Shared.ValueObjects
{
    public record Money
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value < 0m)
            {
                throw new MoneyBelowZeroException();
            }

            Value = value;
        }

        public override string ToString() => Value.ToString();

        public static implicit operator decimal?(Money value) => value is not null ? value.Value : null;

        public static implicit operator Money(decimal value) => new(value);
    }
}
