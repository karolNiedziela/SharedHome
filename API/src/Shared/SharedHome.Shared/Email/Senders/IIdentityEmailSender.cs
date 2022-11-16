namespace SharedHome.Shared.Email.Senders
{
    public interface IIdentityEmailSender
    {
        Task SendAsync(string email, string code);
    }
}
