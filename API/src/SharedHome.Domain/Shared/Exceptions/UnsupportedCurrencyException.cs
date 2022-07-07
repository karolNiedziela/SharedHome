using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Shared.Exceptions
{
    public class UnsupportedCurrencyException : SharedHomeException
    {
        public override string ErrorCode => "UnsupportedCurrency";

        [Order]
        public string Currency { get; }

        public UnsupportedCurrencyException(string currency) : base($"Currency '{currency}' is unsupported.")
        {
            Currency = currency;
        }
    }
}
