namespace SharedHome.Shared.Abstractions.Exceptions
{
    public class InvalidEnumException : SharedHomeException
    {
        public InvalidEnumException() : base("Invalid enum value.")
        {

        }
    }
}
