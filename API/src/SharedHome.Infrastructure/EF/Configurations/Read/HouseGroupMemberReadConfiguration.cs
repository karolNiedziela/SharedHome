using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.Configurations.Read
{
    internal class HouseGroupMemberReadConfiguration : IEntityTypeConfiguration<HouseGroupMemberReadModel>
    {
        public void Configure(EntityTypeBuilder<HouseGroupMemberReadModel> builder)
        {
            builder.ToTable("HouseGroupMember");

            builder.HasKey(member => new { member.HouseGroupId, member.PersonId });

            builder.HasOne(member => member.Person)
                   .WithOne(person => person.HouseGroupMember)
                   .HasForeignKey<HouseGroupMemberReadModel>(member => member.PersonId)
                   .IsRequired(false);
        }
    }
}
