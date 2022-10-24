using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Notifications.Entities;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class AppNotificationFieldWriteConfiguration : IEntityTypeConfiguration<AppNotificationField>
    {
        public void Configure(EntityTypeBuilder<AppNotificationField> builder)
        {
            builder.ToTable("NotificationFields");

            builder.HasKey(x => x.Id);

            builder.Property(notification => notification.Type)
                  .HasColumnName("FieldType")
                  .HasConversion<int>();

            builder.Property(notification => notification.Value)
                   .HasColumnName("FieldValue");
        }
    }
}
