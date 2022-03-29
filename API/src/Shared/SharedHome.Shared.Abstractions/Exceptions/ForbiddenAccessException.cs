namespace SharedHome.Shared.Abstractions.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base("You don't have permission.")
        {

        }
    }
}
