namespace SharedHome.Shared.Email.Senders
{
    public interface IIdentityEmailSender<T> where T: class, IIdentityEmailSender<T>
    {
        Task SendAsync(string email, string code);
    }
}
