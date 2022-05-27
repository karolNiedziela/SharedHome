using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class TooLongShoppingListNameException : SharedHomeException
    {
        public override string ErrorCode => "TooLongShoppingListName";

        public int MaximumLength { get; }

        public TooLongShoppingListNameException(int maximumLength) 
            : base($"Name can contain maximum {maximumLength} characters.")
        {
            MaximumLength = maximumLength;
        }

    }
}
