using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class EmptyNetContentValueException : SharedHomeException
    {
        public override string ErrorCode => "EmptyNetContentValueException";

        public EmptyNetContentValueException() : base("Net content value cannot be empty.")
        {

        }
    }
}
