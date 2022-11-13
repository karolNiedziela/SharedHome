namespace SharedHome.Infrastructure.EF.Options
{
    public class MySQLOptions
    {
        public string ConnectionString { get; set; } = default!;

        public int MaxRetryCount { get; set; }

        public int CommandTimeout { get; set; }

        public bool EnableDetailedErrors { get; set; }

        public bool EnableSensitiveDataLogging { get; set; }
    }
}
