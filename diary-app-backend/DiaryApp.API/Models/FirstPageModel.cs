using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Models
{
    public class FirstPageModel
    {
        public int ID { get; set; }
        public int Month { get; set; }
        public List<TodoItemModel> ThingsTodo { get; set; }
        public List<EventViewModelLight> ImportantEvents { get; set; }
    }
}
