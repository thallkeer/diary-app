using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public abstract class ListItemBase
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Subject { get; set; } = string.Empty;
        [Required]
        public int EventListID { get; set; }
        public virtual EventList Owner { get; set; }
    }
}
