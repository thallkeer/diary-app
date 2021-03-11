using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Interfaces;

namespace DiaryApp.Core.Entities
{
    public class IdeasArea : MonthPageArea, IMonthPageArea<IdeasArea>
    {
        private const string HeaderSTR = "Идеи этого месяца";

        public IdeasArea()
        {}

        public IdeasArea(MonthPage page, bool needInit) : base(page, HeaderSTR, needInit)
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
