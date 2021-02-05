using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities
{
    public class ImportantThingsArea : PageAreaBase<MainPage>
    {
        private const string Title = "Важные дела";

        public ImportantThingsArea() : base()
        {}

        public ImportantThingsArea(MainPage page, bool withInitialization = false) : base(page, Title, withInitialization)
        {}

        [Required]
        public int ImportantThingsID { get; set; }
        public virtual TodoList ImportantThings { get; set; }

        protected override void Initialize()
        {
            ImportantThings = new TodoList(Title);
        }
    }
}
