using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Interfaces;

namespace DiaryApp.Core.Entities.PageAreas
{
    public class IdeasArea : MonthPageArea, IMonthPageArea<IdeasArea>
    {
        private const string HeaderStr = "Идеи этого месяца";

        public IdeasArea()
        {}

        public IdeasArea(MonthPage page, bool needInit) : base(page, HeaderStr, needInit)
        {}

        public virtual IdeasList IdeasList { get; set; }

        public void AddDataFromOtherArea(IdeasArea other)
        {
            if (IdeasList == null)
                Initialize();
            var otherListItemsCopy = other.IdeasList.CopyItems();
            IdeasList.Items.AddRange(otherListItemsCopy);
        }

        protected override void Initialize()
        {
            IdeasList = new IdeasList(string.Empty);
        }
    }
}
