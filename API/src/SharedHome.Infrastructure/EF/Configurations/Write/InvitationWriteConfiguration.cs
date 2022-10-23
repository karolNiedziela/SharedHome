using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.ValueObjects;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class InvitationWriteConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable("Invitations");

            builder.HasKey(invitation => invitation.Id);

            builder.Property(invitation => invitation.Id)
                .HasConversion(invitationId => invitationId.Value, id => new InvitationId(id));

            builder.Property(invitation => invitation.Status)
                   .HasColumnName("InvitationStatus")
                   .HasConversion<int>();

            builder.Property(invitation => invitation.RequestedByPersonId)
                   .HasConversion(personId => personId.Value, id => new PersonId(id));

            builder.Property(invitation => invitation.RequestedToPersonId)
                   .HasConversion(personId => personId.Value, id => new PersonId(id));

            builder.Property(invitation => invitation.HouseGroupId)
                   .HasConversion(houseGroupId => houseGroupId.Value, id => new HouseGroupId(id));

            builder.HasOne<Person>().WithMany().HasForeignKey(invitation => invitation.RequestedByPersonId);

            builder.HasOne<Person>().WithMany().HasForeignKey(invitation => invitation.RequestedToPersonId);

            builder.HasOne<HouseGroup>().WithMany().HasForeignKey(invitation => invitation.HouseGroupId);
        }
    }
}
