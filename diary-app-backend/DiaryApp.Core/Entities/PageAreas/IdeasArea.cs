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

        public void AddFromOtherArea(IdeasArea other)
        {
            var otherListItemsCopy = other.IdeasList.CopyItems();
            if (IdeasList == null)
                Initialize();
            IdeasList.Items.AddRange(otherListItemsCopy);
        }

        protected override void Initialize()
        {
            IdeasList = new IdeasList(string.Empty);
        }
    }
}
