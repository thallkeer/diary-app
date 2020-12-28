using DiaryApp.Core.Models.PageAreas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models.Pages
{
    public class WeekPage : PageBase
    {
        [Required]
        public virtual WeekPlansArea WeekPlansArea { get; set; }
        [Required] 
        public virtual NotesArea NotesArea { get; set; }

        public WeekPage() : base()
        {
        }

        public WeekPage(int year, int month, AppUser user) : base(year, month, user)
        {
        }

        public override void CreateAreas()
        {
            WeekPlansArea = new WeekPlansArea(this, true);
            NotesArea = new NotesArea(this, true);
        }

        public override bool Equals(object obj)
        {
            return obj is WeekPage page &&
                   base.Equals(obj) &&
                   EqualityComparer<WeekPlansArea>.Default.Equals(WeekPlansArea, page.WeekPlansArea) &&
                   EqualityComparer<NotesArea>.Default.Equals(NotesArea, page.NotesArea);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), WeekPlansArea, NotesArea);
        }
    }
}
