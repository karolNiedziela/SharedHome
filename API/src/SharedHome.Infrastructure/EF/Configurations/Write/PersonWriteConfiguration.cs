using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.Persons;
using SharedHome.Domain.Persons.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class PersonWriteConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(person => person.Id);

            builder.Property(person => person.Id);

            builder.Property(person => person.Id)
                .ValueGeneratedNever()
                .HasConversion(personId => personId.Value, id => new PersonId(id));

            builder.Property(person => person.Email)
                .HasConversion(email => email.Value, value => new Email(value));

            builder.Property(person => person.FirstName)
                .HasConversion(firstName => firstName.Value, value => new FirstName(value));

            builder.Property(person => person.LastName)
                .HasConversion(lastName => lastName.Value, value => new LastName(value));
        }
    }
}
