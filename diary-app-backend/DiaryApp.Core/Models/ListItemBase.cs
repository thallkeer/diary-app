using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public abstract class ListItemBase<TOwner>
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Subject { get; set; } = string.Empty;
        [Required]
        public int OwnerID { get; set; }
        public virtual TOwner Owner { get; set; }
    }
}
