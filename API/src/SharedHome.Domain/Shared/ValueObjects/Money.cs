using SharedHome.Domain.Shared.Exceptions;

namespace SharedHome.Domain.Shared.ValueObjects
{
    public record Money
    {
        public Currency Currency { get; } = default!;

        public decimal Amount { get; }

        private Money()
        {

        }

        public Money(decimal amount, Currency currency)
        {
            if (amount < 0m)
            {
                throw new MoneyAmountBelowZeroException();
            }

            Amount = amount;
            Currency = currency;
        }
    }
}
