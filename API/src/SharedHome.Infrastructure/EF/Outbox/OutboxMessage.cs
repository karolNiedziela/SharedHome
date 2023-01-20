namespace SharedHome.Infrastructure.EF.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime OccuredOn { get; set; }

        public DateTime? ProcessedOnUtc { get; set; }

        public string? Error { get; set; }
    }
}
