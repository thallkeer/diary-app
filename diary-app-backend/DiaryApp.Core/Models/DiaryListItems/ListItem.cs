﻿namespace DiaryApp.Core.Models
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
}