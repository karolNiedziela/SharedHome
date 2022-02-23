using SharedHome.Domain.ShoppingLists.Exceptions;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record ProductPrice
    {
        public decimal? Value { get; }

        public ProductPrice(decimal? value)
        {
            if (value.HasValue && value < 0m)
            {
                throw new ProductPriceBelowZeroException();
            }

            Value = value;
        }

        public static implicit operator decimal?(ProductPrice price) => price is not null ? price.Value : null;

        public static implicit operator ProductPrice(decimal? value) => new(value);
    }
}
