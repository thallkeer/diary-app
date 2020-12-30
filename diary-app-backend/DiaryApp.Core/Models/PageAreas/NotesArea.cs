using DiaryApp.Core.Models.Pages;
using System;

namespace DiaryApp.Core.Models.PageAreas
{
    public class NotesArea : PageAreaBase<WeekPage>
    {
        public NotesArea()
        { }

        public NotesArea(WeekPage page, bool init) : base(page, "Заметки", init)
        { }

        public string Note { get; set; }

        protected override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}