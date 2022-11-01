namespace SharedHome.Domain.Common.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent @event);
    }
}