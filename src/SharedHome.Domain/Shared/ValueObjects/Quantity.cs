using SharedHome.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Shared.ValueObjects
{
    public record Quantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (value < 0)
            {
                throw new QuantityBelowZeroException();
            }

            Value = value;
        }

        public static implicit operator int(Quantity quantity) => quantity.Value;

        public static implicit operator Quantity(int value) => new(value);
    }
}
