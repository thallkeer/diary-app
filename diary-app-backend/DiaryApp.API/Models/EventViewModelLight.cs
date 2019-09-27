using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Models
{
    public class EventViewModelLight
    {
        public int EventID { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        protected virtual bool fullDay { get; set; }
        public virtual bool FullDay
        {
            get { return true; }
            set { fullDay = value; }
        }
    }
}
