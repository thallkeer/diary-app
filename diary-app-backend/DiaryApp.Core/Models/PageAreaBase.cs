using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public abstract class PageAreaBase
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        public string Header { get; set; }
    }
}
