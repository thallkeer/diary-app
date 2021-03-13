using DiaryApp.Core.Entities.Users.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiaryApp.Core.Configurations
{
    public class NotificationSettingsConfiguration : IEntityTypeConfiguration<NotificationSettings>
    {
        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.Property(s => s.NotifyAt).HasDefaultValue(NotificationSettings.DefaultNotificationTime);
        }
    }
}