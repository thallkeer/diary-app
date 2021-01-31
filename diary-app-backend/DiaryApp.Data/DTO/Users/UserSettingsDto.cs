using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Data.DTO
{
    public class UserSettingsDto : BaseDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public PageAreaTransferSettingsDto PageAreaTransferSettings { get; set; }
    }
}
