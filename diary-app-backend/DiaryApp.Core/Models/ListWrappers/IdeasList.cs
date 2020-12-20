﻿using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Core.Models
{
    public class IdeasList : DiaryAreaList<CommonList, ListItem, IdeasArea, MonthPage>
    {
        public IdeasList()
        {

        }

        public IdeasList(string title) : base(title)
        {
        }
    }
}
