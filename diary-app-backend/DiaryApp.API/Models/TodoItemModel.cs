using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Models
{
    public class TodoItemModel
    {
        public int ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool Done { get; set; } = false;
    }
}
