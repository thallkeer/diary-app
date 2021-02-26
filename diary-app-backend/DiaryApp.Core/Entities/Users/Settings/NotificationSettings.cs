using System;

namespace DiaryApp.Core.Entities.Users.Settings
{
    public class NotificationSettings : AppSettings
    {
        /// <summary>
        /// Is notifications allowed
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Is needed to notify the day before event
        /// </summary>
        public bool NotifyDayBefore { get; set; } = true;

        /// <summary>
        /// Time of day when notification should be triggered
        /// </summary>
        public TimeSpan NotifyAt { get; set; } = new TimeSpan(10, 0, 0);
    }
}
