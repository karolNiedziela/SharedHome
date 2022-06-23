namespace SharedHome.Infrastructure.EF.Options
{
    public class MySQLSettings
    {
        public const string SectionName = "MySQL";

        public string ConnectionString { get; set; } = default!;
    }
}
