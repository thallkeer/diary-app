using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities.Users.Settings
{
    /// <summary>
    /// Base class for all application user settings
    /// </summary>
    public abstract class AppSettings : BaseEntity
    {
        [Required]
        public int UserSettingsId { get; set; }
        public virtual UserSettings UserSettings { get; set; }
    }
}
