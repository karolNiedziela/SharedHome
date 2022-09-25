namespace SharedHome.Shared.Abstractions.Domain
{
    public interface IAuditable
    {
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}