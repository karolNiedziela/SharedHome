namespace SharedHome.Domain.Primivites
{
    public interface IAggregateRoot
    {
        IReadOnlyList<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
