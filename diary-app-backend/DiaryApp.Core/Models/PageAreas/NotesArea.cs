using DiaryApp.Core.Models.Pages;
using System;

namespace DiaryApp.Core.Models.PageAreas
{
    public class NotesArea : PageAreaBase<WeekPage>
    {
        public string Note { get; set; }

        public NotesArea()
        {

        }

        public NotesArea(WeekPage page, bool init) : base(page, "Заметки", init)
        { }        

        protected override void Initialize()
        {

        }
        public void AddFromOtherArea(NotesArea other)
        {
            throw new System.NotImplementedException();
        }

        public NotesArea TransferAreaData(WeekPage page)
        {
            throw new System.NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is NotesArea area &&
                   base.Equals(obj) &&
                   Note == area.Note;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Note);
        }
    }
}
