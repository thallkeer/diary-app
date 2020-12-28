using DiaryApp.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DiaryApp.Core.Models
{
    public class ListItem : ListItemBase, IDiaryListItem<CommonList, ListItem>
    {
        public virtual CommonList Owner { get; set; }

        public ListItem()
        {

        }

        public ListItem(ListItem original) : base(original)
        { }

        public override ListItemBase GetCopy() => new ListItem(this);

        public override bool Equals(object obj)
        {
            return obj is ListItem item &&
                   base.Equals(obj) &&
                   EqualityComparer<CommonList>.Default.Equals(Owner, item.Owner);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Owner);
        }
    }
}
