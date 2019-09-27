using DiaryApp.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApp.Core
{
    public class EventModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Url { get; set; }
        public bool FullDay { get; set; }
        //[Required]
        //public int PageID { get; set; }
        //public virtual FirstPage Page {get;set;}
    }
}
