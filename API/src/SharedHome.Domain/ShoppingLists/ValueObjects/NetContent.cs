using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Shared.Helpers;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record NetContent
    {
        public string Value { get; } = default!;

        public NetContentType? Type { get; }

        private NetContent()
        {

        }

        public NetContent(string value, NetContentType? type)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyNetContentValueException();
            }

            Value = value;
            Type = type;
        }
    }
}
