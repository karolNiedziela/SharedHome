using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.Persons;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.ValueObjects;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class ShoppingListWriteConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            ConfigureShoppingList(builder);
            ConfigureShoppingListProduct(builder);

            builder.HasOne<Person>().WithMany().HasForeignKey(shoppingList => shoppingList.PersonId);
        }

        private static void ConfigureShoppingList(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.ToTable("ShoppingLists");

            builder.HasKey(shoppingList => shoppingList.Id);

            builder.Property(shoppingList => shoppingList.Id)
                .HasConversion(id => id.Value, id => new ShoppingListId(id));

            builder.Property(shoppingList => shoppingList.PersonId)
                   .HasConversion(personId => personId.Value, id => new PersonId(id));

            builder.OwnsOne(shoppingList => shoppingList.Name, navigation =>
            {
                navigation.Property(name => name.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(20);
            });

            builder.Property(shoppingList => shoppingList.Status)
                   .HasColumnName("Status")
                   .HasConversion<int>();
        }

        private static void ConfigureShoppingListProduct(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.OwnsMany(shoppingList => shoppingList.Products, navigation =>
            {
                navigation.ToTable("ShoppingListProducts");

                navigation.WithOwner().HasForeignKey("ShoppingListId");

                navigation.HasKey(nameof(ShoppingListProduct.Id), "ShoppingListId");

                navigation.Property(product => product.Id)
                          .HasColumnName("ShoppingListProductId")
                          .HasConversion(id => id.Value, id => new ShoppingListProductId(id));

                navigation.OwnsOne(product => product.Name, navigation =>
                {
                    navigation.Property(name => name.Value)
                              .HasColumnName("Name");
                });

                navigation.OwnsOne(product => product.Quantity, navigation =>
                {
                    navigation.Property(quantity => quantity.Value)
                              .HasColumnName("Quantity");
                });

                navigation.OwnsOne(product => product.Price, navigation =>
                {
                    navigation.Property(price => price.Amount)
                              .HasColumnName("Price")
                              .HasPrecision(12, 4);

                    navigation.OwnsOne(price => price.Currency, navigation =>
                    {
                        navigation.Property(currency => currency.Value)
                                  .HasColumnName("Currency");
                    });
                });

                navigation.OwnsOne(product => product.NetContent, navigation =>
                {
                    navigation.Property(netContent => netContent.Value)
                              .HasColumnName("NetContent");

                    navigation.Property(netContent => netContent.Type)
                              .HasColumnName("NetContentType")
                              .HasConversion<int>();
                });

                navigation.Property(product => product.IsBought)
                          .HasDefaultValue(false);
            });

            builder.Metadata.FindNavigation(nameof(ShoppingList.Products))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
