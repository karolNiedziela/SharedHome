namespace SharedHome.Shared.Exceptions.Common
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base("You don't have permission.")
        {

        }
    }
}
