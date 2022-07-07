using SharedHome.Domain.Shared.Exceptions;

namespace SharedHome.Domain.Shared.ValueObjects
{
    public record Currency
    {
        private static readonly HashSet<string> AllowedValues = new()
        {
            "PLN",
            "EUR"
        };

        public string Value { get; } = default!;

        private Currency()
        {

        }

        public Currency(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidCurrencyException();
            }

            value = value.ToUpperInvariant();
            if (!AllowedValues.Contains(value))
            {
                throw new UnsupportedCurrencyException(value);
            }

            Value = value;
        }

        public static implicit operator Currency(string value) => new(value);

        public static implicit operator string(Currency currency) => currency.Value;
    }
}
