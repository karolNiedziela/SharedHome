using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Persons;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class BillWriteConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills");

            builder.HasKey(bill => bill.Id);

            builder.Property(bill => bill.Id)
                .HasConversion(billdId => billdId.Value, id => new BillId(id));

            builder.Property(bill => bill.PersonId)
                 .HasConversion(personId => personId.Value, id => new PersonId(id));

            builder.Property(bill => bill.IsPaid)
                   .HasDefaultValue(false);

            builder.Property(bill => bill.BillType)
                   .HasColumnName("BillType")
                   .HasConversion<int>();

            builder.OwnsOne(bill => bill.ServiceProvider, navigation =>
            {
                navigation.Property(serviceProvider => serviceProvider.Name)
                          .HasColumnName("ServiceProviderName");
            });

            builder.OwnsOne(bill => bill.Cost, navigation =>
            {
                navigation.Property(cost => cost.Amount)
                          .HasColumnName("Cost")
                          .HasPrecision(14, 2);

                navigation.OwnsOne(money => money.Currency, navigation =>
                {
                    navigation.Property(currency => currency.Value)
                              .HasColumnName("Currency");
                });

            });

            builder.Property(bill => bill.DateOfPayment);
        }
    }
}
