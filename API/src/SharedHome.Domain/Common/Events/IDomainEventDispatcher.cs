namespace SharedHome.Domain.Common.Events
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IDomainEvent @event);
    }
}