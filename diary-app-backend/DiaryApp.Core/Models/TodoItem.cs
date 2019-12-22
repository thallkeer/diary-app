using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public class TodoItem : ListItemBase
    {
        public bool Done { get; set; } = false;
        [Required]
        public int TodoListID { get; set; }
        public virtual TodoList Owner { get; set; }
    }
}
