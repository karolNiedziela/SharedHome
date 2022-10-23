﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Notifications.Entities;

namespace SharedHome.Infrastructure.EF.Configurations.Write
{
    public class NotificationWriteConfiguration : IEntityTypeConfiguration<AppNotification>
    {
        public void Configure(EntityTypeBuilder<AppNotification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(notification => notification.Id);

            builder.Property(notification => notification.Type)
                  .HasColumnName("NotificationType")
                  .HasConversion<int>();

            builder.Property(notification => notification.Target)
                  .HasColumnName("TargetType")
                  .HasConversion<int>();

            builder.Property(notification => notification.Operation)
                  .HasColumnName("OperationType")
                  .HasConversion<int>();
        }
    }
}
