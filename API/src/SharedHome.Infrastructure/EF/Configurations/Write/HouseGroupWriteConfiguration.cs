using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class HouseGroupWriteConfiguration : IEntityTypeConfiguration<HouseGroup>
    {
        public void Configure(EntityTypeBuilder<HouseGroup> builder)
        {            
            ConfigureHouseGroup(builder);
            ConfigureHouseGroupMember(builder);  
        }
      
        private void ConfigureHouseGroup(EntityTypeBuilder<HouseGroup> builder)
        {
            builder.ToTable("HouseGroups");

            builder.HasKey(houseGroup => houseGroup.Id);

            builder.Property(houseGroup => houseGroup.Id)
                .HasConversion(houseGroupId => houseGroupId.Value, id => new HouseGroupId(id));


            builder.OwnsOne(houseGroup => houseGroup.Name, navigation =>
            {
                navigation.Property(name => name.Value)
                          .HasColumnName("Name")
                          .HasMaxLength(30);
            });
        }

        private void ConfigureHouseGroupMember(EntityTypeBuilder<HouseGroup> builder)
        {
            builder.OwnsMany(houseGroup => houseGroup.Members, navigation =>
            {
                navigation.ToTable("HouseGroupMembers");

                navigation.WithOwner().HasForeignKey("HouseGroupId");

                navigation.HasKey("Id");

                navigation.Property(member => member.PersonId)
                 .HasConversion(personId => personId.Value, id => new PersonId(id));

                navigation.Property(member => member.HouseGroupId)
                    .HasConversion(houseGroupId => houseGroupId.Value, id => new HouseGroupId(id));

                navigation.Property(member => member.IsOwner)
                          .HasDefaultValue(false);
            });

            builder.Metadata.FindNavigation(nameof(HouseGroup.Members))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
