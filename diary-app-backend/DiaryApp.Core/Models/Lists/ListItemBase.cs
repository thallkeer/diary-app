using DiaryApp.Core.Models.Lists;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core
{
    public abstract class ListItemBase
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Url { get; set; }
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public int OwnerID { get; set; }
        [NotMapped]
        public virtual object Owner { get; set; }

        public ListItemBase()
        {

        }

        public ListItemBase(ListItemBase original)
        {
            Subject = original.Subject;
            Url = original.Url;
        }

        public abstract ListItemBase GetCopy();

        public override string ToString()
        {
            return Subject;
        }
    }    
}
