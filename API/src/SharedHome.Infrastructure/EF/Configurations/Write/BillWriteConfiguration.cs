using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Persons.Aggregates;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class BillWriteConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill");

            builder.HasKey(bill => bill.Id);

            builder.Property(bill => bill.PersonId)
                .IsRequired();

            builder.Property(bill => bill.IsPaid)
                   .HasDefaultValue(false);

            builder.Property(bill => bill.BillType)
                   .HasConversion<string>();

            builder.OwnsOne(bill => bill.ServiceProvider, navigation =>
            {
                navigation.Property(serviceProvider => serviceProvider.Name)
                          .HasColumnName("ServiceProviderName")
                          .IsRequired();
            });

            builder.OwnsOne(bill => bill.Cost, navigation =>
            {
                navigation.Property(cost => cost.Value)
                          .HasColumnName("Cost")
                          .HasPrecision(14, 2)
                          .IsRequired();

            });

            builder.Property(bill => bill.DateOfPayment)
                   .IsRequired();

            builder.HasOne<Person>().WithMany().HasForeignKey(bill => bill.PersonId);
        }
    }
}
