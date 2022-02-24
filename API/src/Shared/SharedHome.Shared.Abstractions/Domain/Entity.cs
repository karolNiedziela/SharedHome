using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Domain
{
    public abstract class Entity
    {
        private readonly List<IEvent> _events = new();

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public IEnumerable<IEvent> Events => _events;

        public void ClearEvents() => _events.Clear();

        protected void AddEvent(IEvent @event)
        {
            if (!_events.Any())
            {
                _events.Add(@event);
            }
        }
    }
}
