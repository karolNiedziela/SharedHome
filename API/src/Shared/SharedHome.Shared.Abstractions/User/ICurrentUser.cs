namespace SharedHome.Shared.Abstractions.User
{
    public interface ICurrentUser
    {
        Guid UserId { get; }

        string FirstName { get; }

        string LastName { get; }

        string Email { get; }

        Dictionary<string, IEnumerable<string>> Claims { get; }
    }
}
