namespace SharedHome.Shared.Abstractions.User
{
    public interface ICurrentUser
    {
        string UserId { get; }

        string FirstName { get; }

        string LastName { get; }

        string Email { get; }

        Dictionary<string, IEnumerable<string>> Claims { get; }
    }
}
