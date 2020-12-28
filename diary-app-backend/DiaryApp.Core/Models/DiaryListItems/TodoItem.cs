using DiaryApp.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DiaryApp.Core.Models
{
    public class TodoItem : ListItemBase, IDiaryListItem<TodoList, TodoItem>
    {
        /// <summary>
        /// Is thing is done
        /// </summary>
        public bool Done { get; set; }
        public virtual TodoList Owner { get; set; }

        public TodoItem()
        {

        }

        public TodoItem(TodoItem original) : base(original)
        {
            this.Done = original.Done;
        }

        public override ListItemBase GetCopy() => new TodoItem(this);

        public override bool Equals(object obj)
        {
            return obj is TodoItem item &&
                   base.Equals(obj) &&
                   Done == item.Done &&
                   EqualityComparer<TodoList>.Default.Equals(Owner, item.Owner);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Done, Owner);
        }
    }
}
