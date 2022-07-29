using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Shared.Helpers;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record NetContent
    {
        public string? Value { get; private set; }

        public NetContentType? Type { get; private set; }

        private NetContent()
        {

        }

        public NetContent(string? value, NetContentType? type)
        {
            Value = value;
            Type = type;
        }
    }
}
