using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.Configurations.Read
{
    internal class PersonReadConfiguration : IEntityTypeConfiguration<PersonReadModel>
    {
        public void Configure(EntityTypeBuilder<PersonReadModel> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(person => person.Id);

            builder.HasMany(person => person.ShoppingLists)
                .WithOne(shoppingList => shoppingList.Person)
                .HasForeignKey(shoppingList => shoppingList.PersonId);

            builder.HasMany(person => person.Bills)
                .WithOne(bill => bill.Person)
                .HasForeignKey(bill => bill.PersonId);
        }
    }
}
