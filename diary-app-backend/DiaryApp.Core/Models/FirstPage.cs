using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApp.Core.Models
{
    public class FirstPage
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [Range(1,12)]
        public int Month { get; set; }
        public virtual List<TodoItem> ThingsTodo { get; set; }
        public virtual List<EventModel> ImportantEvents { get; set; }
    }
}
