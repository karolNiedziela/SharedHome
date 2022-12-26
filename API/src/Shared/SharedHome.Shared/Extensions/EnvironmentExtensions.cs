namespace SharedHome.Shared.Extensions
{
    public static class EnvironmentExtensions
    {
        public const string Development = "Development";

        public const string Test = "Test";

        public const string Production = "Production";

        public static bool IsDevelopment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Development;

        public static bool IsTest => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Test;

        public static bool IsProduction => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Production;
    }
}
