using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.Shared.ValueObjects;
using System.Diagnostics;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class HouseGroupWriteConfiguration : IEntityTypeConfiguration<HouseGroup>
    {
        public void Configure(EntityTypeBuilder<HouseGroup> builder)
        {
            builder.ToTable("HouseGroups");

            builder.HasKey(houseGroup => houseGroup.Id);

            builder.Ignore(houseGroup => houseGroup.Members);

            builder.Property(houseGroup => houseGroup.Id)
                .HasConversion(houseGroupId => houseGroupId.Value, id => new HouseGroupId(id));

            builder.OwnsOne(houseGroup => houseGroup.Name, navigation =>
            {
                navigation.Property(name => name.Value)
                          .HasColumnName("Name")
                          .IsRequired();
            });

            builder.OwnsMany(houseGroup => houseGroup.Members, navigation =>
            {
                navigation.WithOwner().HasForeignKey("HouseGroupId");

                navigation.ToTable("HouseGroupMembers");

                navigation.Property(member => member.PersonId)
                 .HasConversion(personId => personId.Value, id => new PersonId(id));

                navigation.Property(member => member.HouseGroupId)
                    .HasConversion(houseGroupId => houseGroupId.Value, id => new HouseGroupId(id));

                navigation.HasKey(member => new { member.HouseGroupId, member.PersonId});

                navigation.Property(member => member.IsOwner)
                          .HasDefaultValue(false);

                navigation.HasOne<Person>().WithOne().HasForeignKey<HouseGroupMember>(member => member.PersonId);
            });
        }
    }
}
