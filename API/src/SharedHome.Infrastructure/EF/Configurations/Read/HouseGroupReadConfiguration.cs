using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.Configurations.Read
{
    internal class HouseGroupReadConfiguration : IEntityTypeConfiguration<HouseGroupReadModel>
    {
        public void Configure(EntityTypeBuilder<HouseGroupReadModel> builder)
        {
            builder.ToTable("HouseGroup");

            builder.HasKey(houseGroup => houseGroup.Id);

            builder.HasMany(houseGroup => houseGroup.Members)
                   .WithOne(member => member.HouseGroup)
                   .HasForeignKey(member => member.HouseGroupId);
        }
    }
}
