using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.HouseGroups.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF.Configurations
{
    public class HouseGroupConfiguration : IEntityTypeConfiguration<HouseGroup>
    {
        public void Configure(EntityTypeBuilder<HouseGroup> builder)
        {
            builder.HasKey(houseGroup => houseGroup.Id);

            builder.Ignore(houseGroup => houseGroup.TotalMembers);

            builder.Ignore(houseGroup => houseGroup.Members);

            builder.OwnsMany(houseGroup => houseGroup.Members, navigation =>
            {
                navigation.ToTable("HouseGroupMembers");

                navigation.Property(member => member.PersonId)
                          .IsRequired();

                navigation.OwnsOne(member => member.FirstName, navigation =>
                {
                    navigation.Property(firstName => firstName.Value)
                              .HasColumnName("FirstName")
                              .IsRequired();
                });

                navigation.OwnsOne(member => member.LastName, navigation =>
                {
                    navigation.Property(lastName => lastName.Value)
                              .HasColumnName("LastName")
                              .IsRequired();
                });

                navigation.OwnsOne(member => member.Email, navigation =>
                {
                    navigation.Property(email => email.Value)
                              .HasColumnName("Email")
                              .IsRequired();
                });

                navigation.Ignore(member => member.FullName);

                navigation.Property(member => member.IsOwner)
                          .HasDefaultValue(false);
            });
        }
    }
}
