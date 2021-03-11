
namespace DiaryApp.Core.Entities
{
    public class TodoItem : DiaryListItem
    {
        public TodoItem() : base()
        {}

        public TodoItem(TodoItem original) : base(original)
        {
            Done = original.Done;
        }

        /// <summary>
        /// Is thing is done
        /// </summary>
        public bool Done { get; set; }

        public virtual TodoList Owner { get; set; }

        public override DiaryListItem GetCopy() => new TodoItem(this);
    }
}
