using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public class MainPage : PageBase
    {
        public int? TodoListID { get; set; }
        public int? EventListID { get; set; }
        public virtual TodoList TodoList { get; set; }
        public virtual EventList EventList { get; set; }
    }
}
