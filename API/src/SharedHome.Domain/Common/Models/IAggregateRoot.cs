namespace SharedHome.Domain.Common.Models
{
    public interface IAggregateRoot<T>
    {
        public T Id { get; }
    }
}
