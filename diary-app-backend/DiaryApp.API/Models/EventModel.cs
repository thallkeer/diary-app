using DiaryApp.Core.Models;
using System;

namespace DiaryApp.API.Models
{
    public class EventModel : ListItemBase<EventListModel>
    {
        public DateTime Date { get; set; }
    }
}
