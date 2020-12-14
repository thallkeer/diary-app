using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models.PageAreas
{
    public class ImportantThingsArea : PageAreaBase<MainPage>
    {
        private const string Title = "Важные дела";

        [Required]
        public int ImportantThingsID { get; set; }
        public virtual TodoList ImportantThings { get; set; }

        public ImportantThingsArea() : base()
        {

        }

        public ImportantThingsArea(MainPage page, bool withInitialization = false) : base(page, Title, withInitialization)
        {

        }

        public override void Initialize()
        {
            ImportantThings = new TodoList(Title);
        }
    }
}
