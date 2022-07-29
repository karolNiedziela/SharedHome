using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.ShoppingLists.Aggregates;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class ShoppingListWriteConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.ToTable("ShoppingList");

            builder.HasKey(shoppingList => shoppingList.Id);

            builder.Property(shoppingList => shoppingList.PersonId)
                   .IsRequired();

            builder.OwnsOne(shoppingList => shoppingList.Name, navigation =>
            {
                navigation.Property(name => name.Name)
                    .HasColumnName("Name")
                    .IsRequired();
            });

            builder.Property(shoppingList => shoppingList.IsDone)
                   .HasDefaultValue(false);

            builder.OwnsMany(shoppingList => shoppingList.Products, navigation =>
            {
                navigation.ToTable("ShoppingListProduct");

                navigation.Property<int>("Id");

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
                    navigation.Property(price => price.Amount)
                              .HasColumnName("Price")
                              .HasPrecision(12, 4)
                              .IsRequired();

                    navigation.OwnsOne(price => price.Currency, navigation =>
                    {
                        navigation.Property(currency => currency.Value)
                                  .HasColumnName("Currency")
                                  .IsRequired();
                    });
                });

                navigation.OwnsOne(product => product.NetContent, navigation =>
                {                    
                    navigation.Property(netContent => netContent.Value)
                              .HasColumnName("NetContent")
                              .IsRequired();

                    navigation.Property(netContent => netContent.Type)
                              .HasColumnName("NetContentType")
                              .HasConversion<int>();
                });

                navigation.Property(product => product.IsBought)
                          .HasDefaultValue(false);
            });

            builder.HasOne<Person>().WithMany().HasForeignKey(shoppingList => shoppingList.PersonId);
        }
    }
}
