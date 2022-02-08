using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.ShoppingLists.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.HasKey(shoppingList => shoppingList.Id);

            builder.Property(shoppingList => shoppingList.PersonId)
                   .IsRequired();

            builder.OwnsOne(shoppingList => shoppingList.Name, navigation =>
            {
                navigation.Property(name => name.Name)
                    .HasColumnName("Name")
                    .IsRequired();
            });

            builder.OwnsMany(shoppingList => shoppingList.Products, navigation =>
            {
                navigation.ToTable("ShoppingListProducts");

                navigation.OwnsOne(product => product.Name, navigation =>
                {
                    navigation.Property(name => name.Value)
                              .HasColumnName("Name")
                              .IsRequired();
                });

                navigation.OwnsOne(product => product.Quantity, navigation =>
                {
                    navigation.Property(quantity => quantity.Value)
                              .HasColumnName("Quantity")
                              .IsRequired();
                });

                navigation.OwnsOne(product => product.Price, navigation =>
                {
                    navigation.Property(price => price.Value)
                              .HasColumnName("Price")
                              .IsRequired(false);
                });

                navigation.Property(product => product.IsBought)
                          .HasDefaultValue(false);
            });

            builder.Property(shoppingList => shoppingList.IsDone)
                   .HasDefaultValue(false);
        }
    }
}
