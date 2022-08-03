namespace SharedHome.Shared.MySQL
{
    public class MySQLSettings
    {
        public const string SectionName = "MySQL";

        public string ConnectionString { get; set; } = default!;
    }
}
