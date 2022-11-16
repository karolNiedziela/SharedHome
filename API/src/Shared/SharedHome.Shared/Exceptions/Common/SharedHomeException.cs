using System.Net;

namespace SharedHome.Shared.Exceptions.Common
{
    public abstract class SharedHomeException : Exception
    {
        public abstract string ErrorCode { get; }

        public abstract HttpStatusCode StatusCode { get; }

        protected SharedHomeException(string message) : base(message)
        {

        }
    }
}
