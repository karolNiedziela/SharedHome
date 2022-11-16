namespace SharedHome.Shared.Exceptions.Common
{
    public sealed class IdentityException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public IdentityException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
