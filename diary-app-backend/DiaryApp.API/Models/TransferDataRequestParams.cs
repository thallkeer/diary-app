using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Models;

namespace DiaryApp.API.Models
{
    public class TransferDataRequestParams
    {
        [Required]
        public PageRequest PageParams { get; set; }
        [Required]
        public TransferDataModel TransferDataModel { get; set; }
    }
}
