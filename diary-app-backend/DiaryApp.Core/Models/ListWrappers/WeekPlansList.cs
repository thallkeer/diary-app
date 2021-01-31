using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core.Models
{
    public class WeekPlansList : BaseEntity
    {
        public WeekPlansList()
        { }

        public WeekPlansList(string title)
        {
            List = new TodoList(title);
        }

        [Required]
        public int ListID { get; set; }
        public virtual TodoList List { get; set; }
        [NotMapped]
        public List<TodoItem> Items { get => List.Items; set => List.Items = value; }
        [Required]
        public int WeekPlansAreaID { get; set; }
        public virtual WeekPlansArea WeekPlansArea { get; set; }
    }
}