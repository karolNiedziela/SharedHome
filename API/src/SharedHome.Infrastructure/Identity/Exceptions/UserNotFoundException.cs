using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Infrastructure.Identity.Exceptions
{
    internal class UserNotFoundException : SharedHomeException
    {
        public string UserId { get; }

        public UserNotFoundException(string userId) : base($"User with id: '{userId}' was not found.")
        {
            UserId = userId;
        }

    }
}
