namespace SharedHome.Shared.Email.Options
{
    public class EmailOptions
    {
        public string Host { get; set; } = default!;

        public int Port { get; set; }

        public string Address { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
