using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Models.Pages;

namespace DiaryApp.Core.Models.PageAreas
{
    public class NotesArea : PageAreaBase<WeekPage>
    {
        public string Note { get; set; }
        public PageAreaType AreaType => PageAreaType.Notes;

        public NotesArea()
        {

        }

        public NotesArea(WeekPage page, bool init) : base(page, "Заметки", init)
        { }        

        public override void Initialize()
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
