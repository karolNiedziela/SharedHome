using SharedHome.Domain.Common.Events;

namespace SharedHome.Domain.Common.Models
{
    public abstract class Entity : IAuditable
    {
        private readonly List<IDomainEvent> _events = new();

        public IEnumerable<IDomainEvent> Events => _events;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = default!;

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; } = default!;

        public void ClearEvents() => _events.Clear();

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any())
            {
                _events.Add(@event);
            }
        }
    }
}
