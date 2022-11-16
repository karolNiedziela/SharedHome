namespace SharedHome.Shared.Email.Senders
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage email);
    }
}
