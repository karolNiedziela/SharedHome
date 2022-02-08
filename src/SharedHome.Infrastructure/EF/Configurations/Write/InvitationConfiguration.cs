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
        }
    }
}
