﻿using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core.Models.Lists
{
    public class PurchasesList : ITodoListWrapper
    {
        public int ID { get; set; }
        [Required]
        public int ListID { get; set; }
        public virtual TodoList List { get; set; }
        [NotMapped]
        public List<TodoItem> Items { get => List.Items; set => List.Items = value; }
        [Required]
        public int PurchasesAreaID { get; set; }
        public virtual PurchasesArea PurchasesArea { get; set; }

        public PurchasesList()
        {

        }

        public PurchasesList(string title)
        {
            List = new TodoList(title);
        }
    }
}