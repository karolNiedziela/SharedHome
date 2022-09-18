using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.Persons.Aggregates;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class PersonWriteConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.Property(person => person.Id).ValueGeneratedNever();

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
