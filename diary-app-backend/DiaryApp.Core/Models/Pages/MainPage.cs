using DiaryApp.Core.Models.PageAreas;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public class MainPage : PageBase
    {
        [Required]
        public virtual ImportantEventsArea ImportantEvents { get; set; }
        [Required]
        public virtual ImportantThingsArea ImportantThings { get; set; }

        public MainPage() : base()
        {

        }

        public MainPage(int year, int month, AppUser user) : base(year, month, user)
        {
        }

        public override void CreateAreas()
        {
            ImportantEvents = new ImportantEventsArea(this, true);
            ImportantThings = new ImportantThingsArea(this, true);
        }
    }
}
