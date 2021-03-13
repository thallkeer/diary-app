using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;

namespace DiaryApp.API.Models
{
    public class TransferDataRequest
    {
        [Required]
        public int OriginalPageId { get; set; }
        [Required]
        public PageAreaTransferSettingsDto TransferDataModel { get; set; }
    }
}
