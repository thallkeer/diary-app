using DiaryApp.Core.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core
{
    public class EventItem : ListItemBase
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Url { get; set; }
        public bool FullDay { get; set; }
    }
}
