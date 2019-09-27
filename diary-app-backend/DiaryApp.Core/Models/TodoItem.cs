using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApp.Core.Models
{
    public class TodoItem
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Subject { get; set; } = string.Empty;
        public bool Done { get; set; } = false;
        [Required]
        public int OwnerListID { get; set; }
        public virtual TodoList Owner {get;set;}
    }
}
