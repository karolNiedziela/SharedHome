namespace SharedHome.Shared.Abstractions.Domain
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent @event);
    }
}
