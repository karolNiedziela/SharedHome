namespace SharedHome.Shared.Abstractions.Exceptions
{
    public abstract class SharedHomeException : Exception
    {
        public abstract string ErrorCode { get; }

        protected SharedHomeException(string message) : base(message)
        {

        }
    }
}
