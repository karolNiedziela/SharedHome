using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.Invitations.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(invitation => new { invitation.HouseGroupId, invitation.PersonId });

            builder.Property(invitation => invitation.Status)
                   .HasConversion<string>();

            builder.OwnsOne(invitation => invitation.SentByFirstName, navigation =>
            {
                navigation.Property(sentByFirstName => sentByFirstName.Value)
                          .HasColumnName("SentByFirstName")
                          .IsRequired();
            });

            builder.OwnsOne(invitation => invitation.SentByLastName, navigation =>
            {
                navigation.Property(sentByLastName => sentByLastName.Value)
                          .HasColumnName("SentByLastName")
                          .IsRequired();
            });
        }
    }
}
