using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Identity.Exceptions
{
    public class UserNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "UserNotFound";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        [Order]
        public string UserId { get; }

        public UserNotFoundException(string userId) : base($"User with id: '{userId}' was not found.")
        {
            UserId = userId;
        }

    }
}
