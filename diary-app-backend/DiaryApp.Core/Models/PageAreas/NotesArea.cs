using DiaryApp.Core.Models.Pages;

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
    }
}
