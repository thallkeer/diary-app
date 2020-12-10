﻿using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core.Models
{
    public class DesiresList : BaseEntity, ICommonListWrapper
    {
        [Required]
        public int ListID { get; set; }
        public virtual CommonList List { get; set; }
        [NotMapped]
        public List<ListItem> Items { get => List.Items; set => List.Items = value; }
        [Required]
        public int DesiresAreaID { get; set; }
        public virtual DesiresArea DesiresArea { get; set; }

        public DesiresList()
        {
        }

        public DesiresList(string title)
        {
            List = new CommonList(title);
        }
    }
}
