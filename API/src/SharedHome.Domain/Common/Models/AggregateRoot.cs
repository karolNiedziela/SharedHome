namespace SharedHome.Domain.Common.Models
{
    public abstract class AggregateRoot<T> : Entity, IAggregateRoot<T>
    {
        public T Id { get; protected set; } = default!;
    }
}
