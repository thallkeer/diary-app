using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Models.Lists;

namespace DiaryApp.Core.Models.PageAreas
{
    public class IdeasArea : PageAreaBase<MonthPage>, IMonthPageArea<IdeasArea>
    {
        private const string HeaderSTR = "Идеи этого месяца";

        public virtual IdeasList IdeasList { get; set; }

        public PageAreaType AreaType => PageAreaType.Ideas;

        public IdeasArea()
        {

        }
        public IdeasArea(MonthPage page, bool needInit = false) : base(page, HeaderSTR, needInit)
        {

        }

        public IdeasArea TransferAreaData(MonthPage page)
        {
            var newArea = new IdeasArea(page)
            {
                IdeasList = new IdeasList
                {
                    List =  this.IdeasList.List.CreateDeepCopy<CommonList, ListItem>()
                }
            };
            return newArea;
        }

        public void AddFromOtherArea(IdeasArea other)
        {
            this.IdeasList.Items.AddRange(other.IdeasList.Items);
        }

        protected override void Initialize()
        {
            IdeasList = new IdeasList(string.Empty);
        }
    }   
}
