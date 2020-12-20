namespace DiaryApp.Core.Models
{
    public class TodoItem : ListItemBase
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
    }
}
