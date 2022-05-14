using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Persons.Aggregates;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class InvitationWriteConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable("Invitation");

            builder.HasKey(invitation => invitation.Id);

            builder.Property(invitation => invitation.Status)
                   .HasConversion<string>();

            builder.HasOne<Person>().WithMany().HasForeignKey(invitation => invitation.RequestedByPersonId);

            builder.HasOne<Person>().WithMany().HasForeignKey(invitation => invitation.RequestedToPersonId);

            builder.HasOne<HouseGroup>().WithMany().HasForeignKey(invitation => invitation.HouseGroupId);
        }
    }
}
