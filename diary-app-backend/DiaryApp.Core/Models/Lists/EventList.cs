﻿using DiaryApp.Core.Models.Lists;

namespace DiaryApp.Core.Models
{
    public class EventList : ListBase<EventItem>
    {
        public EventList()
        {

        }
        public EventList(string title, PageBase page) : base(title, page)
        {
        }
    }
}
