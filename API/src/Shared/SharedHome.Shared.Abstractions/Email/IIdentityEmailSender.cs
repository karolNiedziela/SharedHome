namespace SharedHome.Shared.Abstractions.Email
{
    public interface IIdentityEmailSender
    {
        Task SendAsync(string email, string code);
    }
}
