namespace SharedHome.Shared.Email.Senders
{
    public interface IEmailSender<T> where T : class, IEmailSender<T>
    {
        Task SendAsync(EmailMessage email);
    }
}
