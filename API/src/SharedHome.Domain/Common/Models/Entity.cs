using SharedHome.Domain.Common.Events;

namespace SharedHome.Domain.Common.Models
{
    public abstract class Entity : IAuditableEntity
    {
        private readonly List<IDomainEvent> _events = new();

        public IEnumerable<IDomainEvent> Events => _events;

        public DateTime CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; } = default!;

        public string CreatedByFullName { get; set; } = default!;

        public DateTime? ModifiedAt { get; set; }

        public Guid? ModifiedBy { get; set; }

        public string? ModifiedByFullName { get; set; }        

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
