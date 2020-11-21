using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Models;

namespace DiaryApp.API.Models
{
    public class TransferDataRequestParams
    {
        [Required]
        public PageParams PageParams { get; set; }
        [Required]
        public TransferDataModel TransferDataModel { get; set; }
    }
}
