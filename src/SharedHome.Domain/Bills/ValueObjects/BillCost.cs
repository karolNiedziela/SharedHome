using SharedHome.Domain.Bills.Exceptions;

namespace SharedHome.Domain.Bills.ValueObjects
{
    public record BillCost
    {
        public decimal? Value { get; }

        public BillCost(decimal? value)
        {
            if (value.HasValue && value < 0m)
            {
                throw new BillCostBelowZeroException();
            }

            Value = value;
        }

        public static implicit operator decimal?(BillCost price) => price is not null ? price.Value : null;

        public static implicit operator BillCost(decimal? value) => new(value);
    }
}
