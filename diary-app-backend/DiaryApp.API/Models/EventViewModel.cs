using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Models
{
    public class EventViewModel : EventViewModelLight
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Url { get; set; }
        public override bool FullDay
        {
            get { return fullDay; }
            set { fullDay = value; }
        }
    }
}
