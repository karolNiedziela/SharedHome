namespace SharedHome.Domain.Primivites
{
    public abstract class AggregateRoot<T> : Entity, IAggregateRoot
    {
        private readonly List<IDomainEvent> _events = new();

        public T Id { get; protected set; } = default!;

        public IReadOnlyList<IDomainEvent> Events => _events.ToList();

        protected void AddEvent(IDomainEvent @event) => _events.Add(@event);

        public void ClearEvents() => _events.Clear();
    }
}
