using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core.Models
{
    public abstract class ListItemBase : BaseEntity
    {   
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
