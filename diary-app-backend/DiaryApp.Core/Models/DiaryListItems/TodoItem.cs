
namespace DiaryApp.Core.Models
{
    public class TodoItem : DiaryListItem
    {
        public TodoItem() : base()
        {}

        public TodoItem(TodoItem original) : base(original)
        {
            this.Done = original.Done;
        }

        /// <summary>
        /// Is thing is done
        /// </summary>
        public bool Done { get; set; }

        public virtual new TodoList Owner { get; set; }

        public override DiaryListItem GetCopy() => new TodoItem(this);
    }
}
