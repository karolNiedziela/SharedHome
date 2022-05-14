using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.Persons.Exceptions;
using Shouldly;
using Xunit;

namespace SharedHome.Domain.UnitTests.Persons
{
    public class PersonTests
    {
        private string _personId = "46826ecb-c40d-441c-ad0d-f11e616e4948";

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_Throws_EmptyFirstNameException_When_FirstName_IsNullOrWhiteSpace(string firstName)
        {
            var exception = Record.Exception(() => Person.Create(_personId, firstName, "lastName"));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyFirstNameException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_Throws_EmptyFirstNameException_When_LastName_IsNullOrWhiteSpace(string lastName)
        {
            var exception = Record.Exception(() => Person.Create(_personId, "firstName", lastName));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyLastNameException>();
        }
    }
}
