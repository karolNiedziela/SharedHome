using SharedHome.Domain.Persons.ValueObjects;
using SharedHome.Domain.Primivites;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.Persons
{
    public class Person : AggregateRoot<PersonId>
    {
        public FirstName FirstName { get; private set; } = default!;

        public LastName LastName { get; private set; } = default!;

        public Email Email { get; private set; } = default!;

        private Person()
        {

        }

        private Person(PersonId id, FirstName firstName, LastName lastName, Email email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public static Person Create(PersonId id, FirstName firstName, LastName lastName, Email email)
            => new(id, firstName, lastName, email);
    }
}
