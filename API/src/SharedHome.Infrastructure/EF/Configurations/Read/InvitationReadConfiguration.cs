using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.Configurations.Read
{
    internal class InvitationReadConfiguration : IEntityTypeConfiguration<InvitationReadModel>
    {
        public void Configure(EntityTypeBuilder<InvitationReadModel> builder)
        {
            builder.ToTable("Invitations");

            builder.HasKey(invitation => invitation.Id);

            builder.Property(i => i.Status)
                .HasColumnName("InvitationStatus")
                .IsRequired();

            builder.HasOne(invitation => invitation.HouseGroup)
                   .WithMany(houseGroup => houseGroup.Invitations)  
                   .HasForeignKey(invitation => invitation.HouseGroupId);

            builder.HasOne(invitation => invitation.RequestedByPerson)
                   .WithMany(person => person.SentInvitations)
                   .HasForeignKey(invitation => invitation.RequestedByPersonId);

            builder.HasOne(invitation => invitation.RequestedToPerson)
                   .WithMany(person => person.ReceivedInvitations)
                   .HasForeignKey(invitation => invitation.RequestedToPersonId);
        }
    }
}
