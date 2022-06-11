using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.Persons.Aggregates;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class HouseGroupWriteConfiguration : IEntityTypeConfiguration<HouseGroup>
    {
        public void Configure(EntityTypeBuilder<HouseGroup> builder)
        {
            builder.ToTable("HouseGroup");

            builder.HasKey(houseGroup => houseGroup.Id);

            builder.Ignore(houseGroup => houseGroup.TotalMembers);

            builder.Ignore(houseGroup => houseGroup.Members);

            builder.OwnsMany(houseGroup => houseGroup.Members, navigation =>
            {
                navigation.WithOwner().HasForeignKey("HouseGroupId");
                navigation.ToTable("HouseGroupMember");

                navigation.HasKey(member => new { member.HouseGroupId, member.PersonId });

                navigation.Property(member => member.PersonId)
                          .IsRequired();

                navigation.Property(member => member.IsOwner)
                          .HasDefaultValue(false);

                navigation.HasOne<Person>().WithOne().HasForeignKey<HouseGroupMember>(member => member.PersonId);
            });
        }
    }
}
