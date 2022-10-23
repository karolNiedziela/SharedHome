using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.Configurations.Read
{
    internal class ShoppingListReadConfiguration : IEntityTypeConfiguration<ShoppingListReadModel>
    {
        public void Configure(EntityTypeBuilder<ShoppingListReadModel> builder)
        {
            builder.ToTable("ShoppingLists");
            builder.HasKey(shoppingList => shoppingList.Id);

            builder.HasMany(shoppingList => shoppingList.Products)
                .WithOne(product => product.ShoppingList);

            builder.HasOne(shoppingList => shoppingList.Person)
                .WithMany(person => person.ShoppingLists)
                .HasForeignKey(shoppingList => shoppingList.PersonId);
        }
    }
}
