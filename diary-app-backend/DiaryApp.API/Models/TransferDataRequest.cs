using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Entities;

namespace DiaryApp.API.Models
{
    public class TransferDataRequest
    {
        [Required]
        public int OriginalPageId { get; set; }
        [Required]
        public TransferDataModel TransferDataModel { get; set; }
    }
}
