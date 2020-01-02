﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    public abstract class ListBase<T>
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }       
        public string Title { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int PageID { get; set; }        
        public virtual PageBase Page { get; set; }
        public virtual List<T> Items { get; set; }
    }

    public class EventList : ListBase<EventItem>
    {
        public int? DesiresAreaID { get; set; }
        public virtual DesiresArea DesiresArea { get; set; }
        public int? IdeasAreaID { get; set; }
        public virtual IdeasArea IdeasArea { get; set; }
    }

    public class TodoList : ListBase<TodoItem>
    {   
        public int? PurchasesAreaID { get; set; }
        public virtual PurchasesArea PurchasesArea { get; set; }
    }
}
