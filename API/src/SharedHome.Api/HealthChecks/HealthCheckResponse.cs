namespace SharedHome.Api.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; set; } = default!;

        public IEnumerable<HealthCheck> HealthChecks { get; set; } = new List<HealthCheck>();

        public TimeSpan Duration { get; set; }
    }
}
