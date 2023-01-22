using SharedHome.Domain.Primivites;

namespace SharedHome.Domain.Persons.Events
{
    public record PersonCreated(Guid PersonId, string Email) : IDomainEvent;
}
