using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.Configurations.Read
{
    internal class BillReadConfiguration : IEntityTypeConfiguration<BillReadModel>
    {
        public void Configure(EntityTypeBuilder<BillReadModel> builder)
        {
            builder.ToTable("Bills");
            builder.HasKey(bill => bill.Id);
        }
    }
}
