﻿using System.Collections.Generic;

namespace DiaryApp.Core.Models
{
    public class TodoList : IListModel<TodoItem>
    {
        public int ID { get; set; }
        public int PageID { get; set; }
        public string Title { get; set; }
        public virtual List<TodoItem> Items { get; set ; }
        public int Month { get; set; }
    }
}
