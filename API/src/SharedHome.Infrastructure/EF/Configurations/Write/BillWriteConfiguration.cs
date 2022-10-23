using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Persons.Aggregates;
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
                .IsRequired();

            builder.Property(bill => bill.IsPaid)
                   .HasDefaultValue(false);

            builder.Property(bill => bill.BillType)
                   .HasColumnName("BillType")
                   .HasConversion<int>();

            builder.Property(bill => bill.PersonId)
                   .HasConversion(personId => personId.Value, id => new PersonId(id));

            builder.OwnsOne(bill => bill.ServiceProvider, navigation =>
            {
                navigation.Property(serviceProvider => serviceProvider.Name)
                          .HasColumnName("ServiceProviderName")
                          .IsRequired();
            });

            builder.OwnsOne(bill => bill.Cost, navigation =>
            {
                navigation.Property(cost => cost.Amount)
                          .HasColumnName("Cost")
                          .HasPrecision(14, 2)
                          .IsRequired();

                navigation.OwnsOne(money => money.Currency, navigation =>
                {
                    navigation.Property(currency => currency.Value)
                              .HasColumnName("Currency")
                              .IsRequired();
                });

            });

            builder.Property(bill => bill.DateOfPayment)
                   .IsRequired();

            builder.HasOne<Person>().WithMany().HasForeignKey(bill => bill.PersonId);
        }
    }
}
