using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Identity.Entities;

namespace SharedHome.Identity.EF.Contexts.Configurations
{
    public class UserImageConfiguration : IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder.HasKey(ui => ui.Id);

            builder.HasOne(ui => ui.User)
                .WithMany(au => au.Images)
                .HasForeignKey(ui => ui.UserId);
        }
    }
}
