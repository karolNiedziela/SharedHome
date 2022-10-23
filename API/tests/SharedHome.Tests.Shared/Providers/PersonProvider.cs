using SharedHome.Domain.Persons.Aggregates;

namespace SharedHome.Tests.Shared.Providers
{
    public static class PersonProvider
    {
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string Email = "firstNameLastName@email.com";
        public static readonly Guid PersonId = new("782784d7-0fee-4d7d-a1ff-68689dd340ef");

        public static Person Get() => Person.Create(PersonId, FirstName, LastName, Email);
    }
}
