using System;

namespace DiaryApp.Core.Entities
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