using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class PersonWriteConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.Property(person => person.Id).ValueGeneratedNever();

            builder.Property(person => person.Id)
                .HasConversion(personId => personId.Value, id => new PersonId(id));

            builder.OwnsOne(person => person.FirstName, navigation =>
            {
                navigation.Property(firstName => firstName.Value)
                          .HasColumnName("FirstName")
                          .IsRequired();
            });

            builder.OwnsOne(person => person.LastName, navigation =>
            {
                navigation.Property(lastName => lastName.Value)
                          .HasColumnName("LastName")
                          .IsRequired();
            });

            builder.OwnsOne(person => person.Email, navigation =>
            {
                navigation.Property(email => email.Value)
                          .HasColumnName("Email")
                          .IsRequired();
            });
        }
    }
}
