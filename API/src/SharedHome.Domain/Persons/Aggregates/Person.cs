﻿using SharedHome.Domain.Persons.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.Persons.Aggregates
{
    public class Person : Entity, IAggregateRoot
    {
        public string Id { get; set; } = default!;

        public FirstName FirstName { get; private set; } = default!;

        public LastName LastName { get; private set; } = default!;

        private Person()
        {

        }

        private Person(string id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public static Person Create(string id, string firstName, string lastName)
            => new(id, firstName, lastName);
    }
}
