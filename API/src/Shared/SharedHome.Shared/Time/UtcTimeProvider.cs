namespace SharedHome.Shared.Time
{
    internal class UtcTimeProvider : ITimeProvider
    {
        public DateTime CurrentDate() => DateTime.UtcNow;        
    }
}
