﻿using DiaryApp.Core.Models.Lists;

namespace DiaryApp.Core
{
    public class ListItem : ListItemBase
    {
        public virtual new CommonList Owner { get; set; }

        public ListItem()
        {

        }

        public ListItem(ListItem original) : base(original)
        { }

        public override ListItemBase GetCopy() => new ListItem(this);
    }

    public class CommonList : ListBase<ListItem>
    {
        public CommonList()
        {
        }

        public CommonList(string title)
        {
            Title = title;
        }        
    }
}
