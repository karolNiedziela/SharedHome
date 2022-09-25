using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Domain
{
    public abstract class Entity : IAuditable
    {
        private readonly List<IDomainEvent> _events = new();

        public IEnumerable<IDomainEvent> Events => _events;

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

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
