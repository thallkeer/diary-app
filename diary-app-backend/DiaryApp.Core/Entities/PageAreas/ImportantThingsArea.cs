using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Core.Entities.PageAreas
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
