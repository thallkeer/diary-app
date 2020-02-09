using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public abstract class ListItemBase<TOwner>
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Url { get; set; }
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public int OwnerID { get; set; }
        public virtual TOwner Owner { get; set; }

        public override string ToString()
        {
            return Subject;
        }
    }
}
