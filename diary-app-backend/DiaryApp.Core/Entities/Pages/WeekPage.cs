using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Entities.Users;

namespace DiaryApp.Core.Entities
{
    public class WeekPage : PageBase
    {
        public WeekPage() : base()
        {}

        public WeekPage(int year, int month, AppUser user) : base(year, month, user)
        {}

        [Required]
        public virtual WeekPlansArea WeekPlansArea { get; set; }
        [Required] 
        public virtual NotesArea NotesArea { get; set; }        

        public override void CreateAreas()
        {
            WeekPlansArea = new WeekPlansArea(this, true);
            NotesArea = new NotesArea(this, true);
        }
    }
}
