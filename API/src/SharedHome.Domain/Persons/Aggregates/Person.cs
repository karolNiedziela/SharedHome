﻿using SharedHome.Domain.Persons.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.Persons.Aggregates
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
